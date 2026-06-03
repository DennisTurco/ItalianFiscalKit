using System.Text.RegularExpressions;
using ItalianFiscalKit.Enums;

namespace ItalianFiscalKit.Utils;

internal class FiscalCodeTokenizer
{
    // 3char -> name; 3char -> surname; 2int -> year; 1char -> month; 2int -> day; 4char -> belfiore (catastal code); 1char -> check code
    internal const string RegexPattern = @"([A-Z]{3})([A-Z]{3})([0-9]{2})([ABCDEHILMPRST]{1})([0-9]{2})(\S{4})([A-Z]{1})";
    private readonly string _cf;
    private readonly Match _match;

    internal FiscalCodeTokenizer(string cf)
    {
        _cf = cf.ToUpper();
        _match = Regex.Match(_cf, RegexPattern);
    }

    internal static Gender GetGenderFromBirthDay(int day)
        => (day >= 1 && day <= 31) ? Gender.Male : Gender.Female;

    internal string GetName()
        => _match.Groups[1].Value;

    internal string GetSurname()
        => _match.Groups[2].Value;

    internal int GetYear()
        => CalculateFullYear(int.Parse(_match.Groups[3].Value));

    internal char GetMonthChar()
        => char.Parse(_match.Groups[4].Value);

    internal int GetMonth()
        => char.Parse(_match.Groups[4].Value) switch
        {
            'A' => 1,
            'B' => 2,
            'C' => 3,
            'D' => 4,
            'E' => 5,
            'H' => 6,
            'L' => 7,
            'M' => 8,
            'P' => 9,
            'R' => 10,
            'S' => 11,
            'T' => 12,
            _ => -1,
        };

    internal int GetStandardDay()
    {
        int day = GetDay();
        return GetGenderFromBirthDay(day) == Gender.Male ? day : day - 40;
    }

    internal int GetDay()
        => int.Parse(_match.Groups[5].Value);

    internal string GetBelfiore()
        => _match.Groups[6].Value;

    internal char GetCheckCode()
        => char.Parse(_match.Groups[7].Value);

    internal static char CalculateCheckCharacter(string partialCF)
    {
        int total = 0;
        for (int i = 0; i < partialCF.Length; i++)
        {
            var entry = DataStore.CheckCodeMap
                .First(e => e.Chars.Any(c => c.Contains(partialCF[i])));

            total += i % 2 == 0 ? entry.Odd : entry.Even;
        }

        int rest = total % 26;

        // convert to char (ascii table)
        char value = (char)(rest + 65);
        return value;
    }

    private static int CalculateFullYear(int shortYear)
        => shortYear <= DateTime.Now.Year % 100 ? 2000 + shortYear : 1900 + shortYear;
}
