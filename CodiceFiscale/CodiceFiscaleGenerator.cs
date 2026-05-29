using CodiceFiscale.Entities;
using CodiceFiscale.Enums;

namespace CodiceFiscale;

/// <summary>
/// Provides methods to generate an Italian Codice Fiscale.
/// </summary>
public class CodiceFiscaleGenerator
{
    /// <summary>
    /// Generates a Codice Fiscale based on the provided personal information.
    /// </summary>
    /// <param name="name">The person's first name.</param>
    /// <param name="surname">The person's surname.</param>
    /// <param name="dateOfBirth">The person's date of birth.</param>
    /// <param name="gender">The person's gender ('M' or 'F').</param>
    /// <returns>The generated Codice Fiscale string.</returns>
    public static string Generate(string name, string surname, DateOnly dateOfBirth, Gender gender)
    {
        return string.Empty;
    }
}
