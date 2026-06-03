using ItalianFiscalKit.Entities;
using ItalianFiscalKit.Enums;
using ItalianFiscalKit.Exceptions;
using ItalianFiscalKit.Utils;

namespace ItalianFiscalKit;

/// <summary>
/// Provides methods to parse an Italian Codice Fiscale into its constituent data.
/// </summary>
public class FiscalCodeParser
{
    /// <summary>
    /// Parses the specified Codice Fiscale and returns the decoded personal data.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string to parse. Case-insensitive.</param>
    /// <returns>
    /// A <see cref="FiscalCodeData"/> instance containing the gender, date of birth,
    /// and Belfiore code extracted from the Codice Fiscale.
    /// </returns>
    /// <exception cref="InvalidFiscalCodeException">
    /// Thrown when <paramref name="cf"/> is not a valid Codice Fiscale.
    /// </exception>
    public static FiscalCodeData Parse(string cf)
    {
        if (!FiscalCodeValidator.IsValid(cf))
            throw new InvalidFiscalCodeException($"The provided Codice Fiscale '{cf}' is not valid");

        FiscalCodeTokenizer cfHelper = new(cf);
        DateOnly date = new(cfHelper.GetYear(), cfHelper.GetMonth(), cfHelper.GetStandardDay());
        string catastalCode = cfHelper.GetBelfiore();
        Gender gender = FiscalCodeTokenizer.GetGenderFromBirthDay(cfHelper.GetDay());

        return new FiscalCodeData(gender, date, catastalCode);
    }

    /// <summary>
    /// Attempts to parse the specified Codice Fiscale without throwing an exception.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string to parse. Case-insensitive.</param>
    /// <param name="data">
    /// When this method returns <see langword="true"/>, contains a <see cref="FiscalCodeData"/>
    /// instance with the gender, date of birth, and Belfiore code extracted from the Codice Fiscale;
    /// otherwise, <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="cf"/> was parsed successfully;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <example>
    /// <code>
    /// if (FiscalCodeParser.TryParse("RSSMRA85T10A562S", out FiscalCodeData? data))
    /// {
    ///     Console.WriteLine($"Gender: {data.Gender}, Born: {data.DateOfBirth}");
    /// }
    /// </code>
    /// </example>
    public static bool TryParse(string cf, out FiscalCodeData? data)
    {
        if (!FiscalCodeValidator.IsValid(cf))
        {
            data = default;
            return false;
        }

        FiscalCodeTokenizer cfHelper = new(cf);
        DateOnly date = new(cfHelper.GetYear(), cfHelper.GetMonth(), cfHelper.GetStandardDay());
        string catastalCode = cfHelper.GetBelfiore();
        Gender gender = FiscalCodeTokenizer.GetGenderFromBirthDay(cfHelper.GetDay());

        data = new FiscalCodeData(gender, date, catastalCode);
        return true;
    }
}
