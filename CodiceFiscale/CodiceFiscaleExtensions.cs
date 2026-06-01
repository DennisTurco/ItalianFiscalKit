using CodiceFiscale.Entities;

namespace CodiceFiscale;

/// <summary>
/// Provides extension methods for working with Italian Codice Fiscale strings and data objects.
/// </summary>
public static class CodiceFiscaleExtensions
{
    /// <summary>
    /// Determines whether this string is a valid Italian Codice Fiscale.
    /// </summary>
    /// <param name="cf">The string to validate.</param>
    /// <returns><c>true</c> if valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidCodiceFiscale(this string cf)
        => CodiceFiscaleValidator.IsValid(cf);

    /// <summary>
    /// Parses this string as an Italian Codice Fiscale.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string to parse.</param>
    /// <returns>The decoded <see cref="CodiceFiscaleData"/>.</returns>
    /// <exception cref="Exceptions.InvalidCodiceFiscaleException">Thrown when the string is not valid.</exception>
    public static CodiceFiscaleData ParseCodiceFiscale(this string cf)
        => CodiceFiscaleParser.Parse(cf);

    /// <summary>
    /// Attempts to parse this string as an Italian Codice Fiscale without throwing.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string to parse.</param>
    /// <param name="data">The parsed data, or <c>null</c> if invalid.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseCodiceFiscale(this string cf, out CodiceFiscaleData? data)
        => CodiceFiscaleParser.TryParse(cf, out data);

    /// <summary>
    /// Normalizes any omocodia letter substitutions in this Codice Fiscale back to digits.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string (may contain omocodia substitutions).</param>
    /// <returns>The normalized Codice Fiscale with digits restored at numeric positions.</returns>
    public static string NormalizeOmocodia(this string cf)
        => CodiceFiscaleNormalizer.NormalizeOmocodia(cf);

    /// <summary>
    /// Gets the current age (in full years) of the person represented by this <see cref="CodiceFiscaleData"/>.
    /// </summary>
    /// <param name="data">The parsed Codice Fiscale data.</param>
    /// <returns>The age in full years as of today.</returns>
    public static int GetAge(this CodiceFiscaleData data)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - data.DateOfBirth.Year;
        if (data.DateOfBirth.AddYears(age) > today)
            age--;
        return age;
    }

    /// <summary>
    /// Determines whether the person represented by this <see cref="CodiceFiscaleData"/> is 18 or older.
    /// </summary>
    /// <param name="data">The parsed Codice Fiscale data.</param>
    /// <returns><c>true</c> if the person is at least 18 years old; otherwise, <c>false</c>.</returns>
    public static bool IsAdult(this CodiceFiscaleData data)
        => data.GetAge() >= 18;
}
