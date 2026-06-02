using CodiceFiscale.Entities;

namespace CodiceFiscale;

/// <summary>
/// Provides extension methods for working with Italian Codice Fiscale strings and data objects.
/// </summary>
public static class CodiceFiscaleExtensions
{
    /// <summary>
    /// Gets the current age (in full years) of the person represented by this <see cref="CodiceFiscaleData"/>.
    /// </summary>
    /// <param name="data">The parsed Codice Fiscale data.</param>
    /// <returns>The age in full years as of today.</returns>
    public static int GetAge(this CodiceFiscaleData data)
    {
        DateTime date = new(data.DateOfBirth.Year, data.DateOfBirth.Month, data.DateOfBirth.Day);
        int years = DateTime.Now.Year - date.Year;
        if (DateTime.Now.Month < date.Month)
            years--;
        return years;
    }

    /// <summary>
    /// Determines whether the person represented by this <see cref="CodiceFiscaleData"/> is 18 or older.
    /// </summary>
    /// <param name="data">The parsed Codice Fiscale data.</param>
    /// <returns><c>true</c> if the person is at least 18 years old; otherwise, <c>false</c>.</returns>
    public static bool IsAdult(this CodiceFiscaleData data)
        => data.GetAge() >= 18;
}
