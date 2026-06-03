using ItalianFiscalKit.Enums;
using ItalianFiscalKit.Exceptions;
using ItalianFiscalKit.Utils;

namespace ItalianFiscalKit;

/// <summary>
/// Provides methods to generate an Italian Fiscal Code.
/// </summary>
public class FiscalCodeGenerator
{
    /// <summary>
    /// Generates a Fiscal Code based on the provided personal information.
    /// </summary>
    /// <param name="name">The person's first name.</param>
    /// <param name="surname">The person's surname.</param>
    /// <param name="dateOfBirth">The person's date of birth.</param>
    /// <param name="gender">The person's gender.</param>
    /// <param name="belfioreCode">The Belfiore (cadastral) code of the person's place of birth.</param>
    /// <returns>The generated 16-character Fiscal Code string.</returns>
    /// <exception cref="InvalidFiscalCodeDataException">
    /// Thrown when <paramref name="name"/>, <paramref name="surname"/>, or <paramref name="belfioreCode"/> is not valid.
    /// </exception>
    public static string Generate(string name, string surname, DateOnly dateOfBirth, Gender gender, string belfioreCode)
    {
        if (string.IsNullOrEmpty(name) || name.Length < 3)
            throw new InvalidFiscalCodeDataException($"The provided Name '{name}' is not valid");
        if (string.IsNullOrEmpty(surname) || surname.Length < 3)
            throw new InvalidFiscalCodeDataException($"The provided Surname '{surname}' is not valid");
        if (string.IsNullOrEmpty(belfioreCode) || belfioreCode.Length != 4 || !IsBelfioreValid(belfioreCode))
            throw new InvalidFiscalCodeDataException($"The provided Belfiore code '{belfioreCode}' is not valid");

        int genderDay = CalculateDayValueByGender(dateOfBirth.Day, gender);
        string stringDay = genderDay.ToString("D2");

        string partialCf =
            GetSurnameCode(surname.ToUpperInvariant()) +
            GetNameCode(name.ToUpperInvariant()) +
            dateOfBirth.Year.ToString()[2..] +
            ConvertToCharMonth(dateOfBirth.Month) +
            stringDay +
            belfioreCode.ToUpperInvariant();

        return partialCf + FiscalCodeTokenizer.CalculateCheckCharacter(partialCf);
    }

    private static int CalculateDayValueByGender(int day, Gender gender)
        => gender == Gender.Male ? day : day + 40;

    private static string GetSurnameCode(string surname)
        => ExtractCode(surname, nameRule: false);

    private static string GetNameCode(string name)
        => ExtractCode(name, nameRule: true);

    private static string ExtractCode(string word, bool nameRule)
    {
        string vowels = "AEIOU";
        var consonants = word.Where(c => char.IsLetter(c) && !vowels.Contains(c)).ToList();
        var vowelList  = word.Where(c => vowels.Contains(c)).ToList();

        List<char> result;

        if (nameRule && consonants.Count >= 4)
        {
            // Name with 4+ consonants: take 1st, 3rd, 4th
            result = [consonants[0], consonants[2], consonants[3]];
        }
        else
        {
            // Surname rule (also name with <4 consonants): consonants first, then vowels, then X
            result = [.. consonants.Take(3)];
            foreach (char v in vowelList)
            {
                if (result.Count >= 3) break;
                result.Add(v);
            }
        }

        // Pad with X if still shorter than 3
        while (result.Count < 3)
            result.Add('X');

        return new string([.. result.Take(3)]);
    }

    private static bool IsBelfioreValid(string belfioreCode)
        => DataStore.Municipalities.Any(x => x.CatastalCode.Equals(belfioreCode, StringComparison.OrdinalIgnoreCase)) ||
           DataStore.ForeignCountries.Any(x => x.CatastalCode.Equals(belfioreCode, StringComparison.OrdinalIgnoreCase));

    private static char ConvertToCharMonth(int month)
        => month switch
        {
            1  => 'A',
            2  => 'B',
            3  => 'C',
            4  => 'D',
            5  => 'E',
            6  => 'H',
            7  => 'L',
            8  => 'M',
            9  => 'P',
            10 => 'R',
            11 => 'S',
            12 => 'T',
            _  => throw new InvalidFiscalCodeDataException($"The provided Month '{month}' is not valid"),
        };
}
