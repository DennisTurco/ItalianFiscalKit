using ItalianFiscalKit.Entities;
using ItalianFiscalKit.Enums;
using ItalianFiscalKit.Exceptions;
using ItalianFiscalKit.Utils;

namespace ItalianFiscalKit;

/// <summary>
/// Provides methods to parse an Italian Codice Fiscale into its constituent data.
/// </summary>
public class CodiceFiscaleParser
{
    /// <summary>
    /// Parses the specified Codice Fiscale and returns the decoded personal data.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string to parse. Case-insensitive.</param>
    /// <returns>
    /// A <see cref="CodiceFiscaleData"/> instance containing the gender, date of birth,
    /// and Belfiore code extracted from the Codice Fiscale.
    /// </returns>
    /// <exception cref="InvalidCodiceFiscaleException">
    /// Thrown when <paramref name="cf"/> is not a valid Codice Fiscale.
    /// </exception>
    public static CodiceFiscaleData Parse(string cf)
    {
        if (!CodiceFiscaleValidator.IsValid(cf))
            throw new InvalidCodiceFiscaleException($"The provided Codice Fiscale '{cf}' is not valid");

        CodiceFiscaleTokenizer cfHelper = new(cf);
        DateOnly date = new(cfHelper.GetYear(), cfHelper.GetMonth(), cfHelper.GetStandardDay());
        string catastalCode = cfHelper.GetBelfiore();
        Gender gender = CodiceFiscaleTokenizer.GetGenderFromBirthDay(cfHelper.GetDay());

        return new CodiceFiscaleData(gender, date, catastalCode);
    }

    /// <summary>
    /// Attempts to parse the specified Codice Fiscale without throwing an exception.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string to parse. Case-insensitive.</param>
    /// <param name="data">
    /// When this method returns <see langword="true"/>, contains a <see cref="CodiceFiscaleData"/>
    /// instance with the gender, date of birth, and Belfiore code extracted from the Codice Fiscale;
    /// otherwise, <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="cf"/> was parsed successfully;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <example>
    /// <code>
    /// if (CodiceFiscaleParser.TryParse("RSSMRA85T10A562S", out CodiceFiscaleData? data))
    /// {
    ///     Console.WriteLine($"Gender: {data.Gender}, Born: {data.DateOfBirth}");
    /// }
    /// </code>
    /// </example>
    public static bool TryParse(string cf, out CodiceFiscaleData? data)
    {
        if (!CodiceFiscaleValidator.IsValid(cf))
        {
            data = default;
            return false;
        }

        CodiceFiscaleTokenizer cfHelper = new(cf);
        DateOnly date = new(cfHelper.GetYear(), cfHelper.GetMonth(), cfHelper.GetStandardDay());
        string catastalCode = cfHelper.GetBelfiore();
        Gender gender = CodiceFiscaleTokenizer.GetGenderFromBirthDay(cfHelper.GetDay());

        data = new CodiceFiscaleData(gender, date, catastalCode);
        return true;
    }
}
