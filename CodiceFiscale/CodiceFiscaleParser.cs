using CodiceFiscale.Entities;
using CodiceFiscale.Enums;
using CodiceFiscale.Exceptions;
using CodiceFiscale.Utils;

namespace CodiceFiscale;

/// <summary>
/// Provides methods to parse an Italian Codice Fiscale into its constituent data.
/// </summary>
public class CodiceFiscaleParser()
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
}
