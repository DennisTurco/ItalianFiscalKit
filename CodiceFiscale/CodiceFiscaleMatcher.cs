using ItalianFiscalKit.Enums;

namespace ItalianFiscalKit;

/// <summary>
/// Provides methods to match and compare Italian Codice Fiscale strings against personal data.
/// </summary>
public class CodiceFiscaleMatcher
{
    /// <summary>
    /// Determines whether the specified Codice Fiscale matches the provided personal data.
    /// The comparison is case-insensitive. Omocodia variants of the CF are normalized before
    /// comparison.
    /// </summary>
    /// <param name="cf">The Codice Fiscale to check.</param>
    /// <param name="name">The person''s first name (minimum 3 characters).</param>
    /// <param name="surname">The person''s surname (minimum 3 characters).</param>
    /// <param name="dateOfBirth">The person''s date of birth.</param>
    /// <param name="gender">The person''s gender.</param>
    /// <param name="belfioreCode">The 4-character Belfiore code of the person''s place of birth.</param>
    /// <returns>
    /// <c>true</c> if the Codice Fiscale is valid and matches the generated CF for the
    /// provided personal data; otherwise, <c>false</c>.
    /// </returns>
    public static bool Matches(string cf, string name, string surname, DateOnly dateOfBirth, Gender gender, string belfioreCode)
    {
        if (string.IsNullOrWhiteSpace(cf))
            return false;

        try
        {
            string expected = CodiceFiscaleGenerator.Generate(name, surname, dateOfBirth, gender, belfioreCode);
            return string.Equals(cf.Trim().Replace(" ", ""), expected, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }
}
