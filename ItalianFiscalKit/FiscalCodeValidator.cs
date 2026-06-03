namespace ItalianFiscalKit;

using System.Text.RegularExpressions;
using ItalianFiscalKit.Utils;

/// <summary>
/// Provides methods to validate Italian Codice Fiscale codes.
/// </summary>
public class FiscalCodeValidator
{
    /// <summary>
    /// Determines whether the specified Codice Fiscale is valid.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string to validate.</param>
    /// <returns><c>true</c> if the Codice Fiscale is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValid(string cf)
    {
        if (string.IsNullOrEmpty(cf) || cf.Length != 16 || cf.Contains(' '))
            return false;

        if (!Regex.IsMatch(cf.ToUpper(), FiscalCodeTokenizer.RegexPattern))
            return false;

        FiscalCodeTokenizer cfHelper = new(cf);
        int year = cfHelper.GetYear();
        char month = cfHelper.GetMonthChar();
        int day = cfHelper.GetDay();
        string catastalCode = cfHelper.GetBelfiore();
        char checkCode = cfHelper.GetCheckCode();

        return
            IsDayValid(year, month, day) &&
            IsCatastalCodeValid(catastalCode) &&
            IsCheckCodeValid(cf.ToUpper(), checkCode);
    }

    private static bool IsCatastalCodeValid(string catastalCode)
    {
        // foreign country codes (Z + 3 digits). check against foreign countries dataset
        if (catastalCode.StartsWith('Z'))
            return DataStore.ForeignCountries.Any(x => x.CatastalCode.Equals(catastalCode, StringComparison.OrdinalIgnoreCase));

        // italian municipality codes
        return DataStore.Municipalities.Any(x => x.CatastalCode.Equals(catastalCode, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsDayValid(int year, char month, int day)
    {
        int minDayMale = 1;
        int maxDayMale;

        if (month == 'S' || month == 'D' || month == 'H' || month == 'P') // 30 days
        {
            maxDayMale = 30;
        }
        else if (month == 'B') // 28/29 days for february
        {
            if (IsLeapYear(year))
            {
                maxDayMale = 29;
            }
            else
            {
                maxDayMale = 28;
            }
        }
        else // 31 days
        {
            maxDayMale = 31;
        }

        int minDayFemale = 41;
        int maxDayFemale = maxDayMale + 40;

        return (day >= minDayMale && day <= maxDayMale) || (day >= minDayFemale && day <= maxDayFemale);
    }

    private static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    private static bool IsCheckCodeValid(string cf, char checkCode)
    {
        string partialCF = cf[..(cf.Length-1)];
        return FiscalCodeTokenizer.CalculateCheckCharacter(partialCF) == checkCode;
    }
}
