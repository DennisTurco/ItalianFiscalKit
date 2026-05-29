using System.Text.RegularExpressions;
using CodiceFiscale.Utils;

namespace CodiceFiscale;

/// <summary>
/// Provides methods to validate Italian Codice Fiscale codes.
/// </summary>
public class CodiceFiscaleValidator
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

        cf = cf.ToUpper();

        // 3char -> name; 3char -> surname; 2int -> year; 1char -> month; 2int -> day; 4char -> belfiore (catastal code); 1char -> check code
        string pattern = @"([A-Z]{3})([A-Z]{3})([0-9]{2})([ABCDEHILMPRST]{1})([0-9]{2})(\S{4})([A-Z]{1})";
        Match match = Regex.Match(cf, pattern);

        if (!Regex.IsMatch(cf, pattern))
            return false;

        int year = int.Parse(match.Groups[3].Value);
        char month = char.Parse(match.Groups[4].Value);
        int day = int.Parse(match.Groups[5].Value);
        string catastalCode = match.Groups[6].Value;
        char checkCode = char.Parse(match.Groups[7].Value);

        return
            IsDayValid(year, month, day) &&
            IsCatastalCodeValid(catastalCode) &&
            IsCheckCodeValid(cf, checkCode);
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
        int fullYear = year <= DateTime.Now.Year % 100 ? 2000 + year : 1900 + year;
        return (fullYear % 4 == 0 && fullYear % 100 != 0) || (fullYear % 400 == 0);
    }

    private static bool IsCheckCodeValid(string cf, char checkCode)
    {
        int total = 0;
        for (int i = 0; i < cf.Length - 1; i++)
        {
            var entry = DataStore.CheckCodeMap
            .First(e => e.Chars.Any(c => c.Contains(cf[i])));

            total += i % 2 == 0 ? entry.Odd : entry.Even;
        }

        int rest = total % 26;

        // convert to char (ascii table)
        char value = (char)(rest + 65);

        return checkCode == value;
    }
}
