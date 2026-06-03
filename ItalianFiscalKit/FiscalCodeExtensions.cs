using ItalianFiscalKit.Entities;

namespace ItalianFiscalKit;

/// <summary>
/// Provides extension methods for working with Italian Fiscal Code strings and data objects.
/// </summary>
public static class FiscalCodeExtensions
{
    /// <summary>
    /// Gets the current age (in full years) of the person represented by this <see cref="FiscalCodeData"/>.
    /// </summary>
    /// <param name="data">The parsed Fiscal Code data.</param>
    /// <returns>The age in full years as of today.</returns>
    public static int GetAge(this FiscalCodeData data)
    {
        DateTime date = new(data.DateOfBirth.Year, data.DateOfBirth.Month, data.DateOfBirth.Day);
        int years = DateTime.Now.Year - date.Year;
        if (DateTime.Now.Month < date.Month)
            years--;
        return years;
    }

    /// <summary>
    /// Determines whether the person represented by this <see cref="FiscalCodeData"/> is 18 or older.
    /// </summary>
    /// <param name="data">The parsed Fiscal Code data.</param>
    /// <returns><c>true</c> if the person is at least 18 years old; otherwise, <c>false</c>.</returns>
    public static bool IsAdult(this FiscalCodeData data)
        => data.GetAge() >= 18;
}
