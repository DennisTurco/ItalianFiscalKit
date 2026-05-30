using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace CodiceFiscale;

/// <summary>
/// Provides methods to validate IBAN (International Bank Account Number) strings.
/// Only SEPA countries are supported (EU27 + UK, Switzerland, Norway, Iceland, Liechtenstein, Monaco, San Marino, Andorra, Vatican City).
/// </summary>
public class IBANValidator
{
    private static readonly string SepaRegexPattern = @"^([A-Z]{2})(\d{2})([A-Z0-9]{11,30})$";
    private static readonly string ITRegexPattern = @"^(IT)(\d{2})([A-Z]{1})([0-9]{22})$";

    private static readonly Dictionary<string, int> SepaLengths = new()
    {
        // EU27
        { "AT", 20 }, { "BE", 16 }, { "BG", 22 }, { "HR", 21 }, { "CY", 28 },
        { "CZ", 24 }, { "DK", 18 }, { "EE", 20 }, { "FI", 18 }, { "FR", 27 },
        { "DE", 22 }, { "GR", 27 }, { "HU", 28 }, { "IE", 22 }, { "IT", 27 },
        { "LV", 21 }, { "LT", 20 }, { "LU", 20 }, { "MT", 31 }, { "NL", 18 },
        { "PL", 28 }, { "PT", 25 }, { "RO", 24 }, { "SK", 24 }, { "SI", 19 },
        { "ES", 24 }, { "SE", 24 },
        // Non-EU SEPA
        { "GB", 22 }, { "CH", 21 }, { "NO", 15 }, { "IS", 26 }, { "LI", 21 },
        { "MC", 27 }, { "SM", 27 }, { "AD", 24 }, { "VA", 22 }
    };

    /// <summary>
    /// Determines whether the specified IBAN string is valid.
    /// Spaces are ignored. Only SEPA country codes are accepted.
    /// </summary>
    /// <param name="iban">The IBAN string to validate. Spaces are allowed and will be stripped.</param>
    /// <returns><c>true</c> if the IBAN is valid and belongs to a SEPA country; otherwise, <c>false</c>.</returns>
    public static bool IsValid(string iban)
    {
        if (string.IsNullOrEmpty(iban) || iban.Length < 15)
            return false;

        iban = iban.ToUpper().Trim().Replace(" ", "");

        if (iban.StartsWith("IT") && !Regex.IsMatch(iban, ITRegexPattern))
            return false;
        if (!Regex.IsMatch(iban, SepaRegexPattern))
            return false;

        string rearranged = iban.Substring(4) + iban.Substring(0, 4);

        StringBuilder builder = new StringBuilder();
        foreach (char c in rearranged)
        {
            if (!int.TryParse(c.ToString(), out int _))
                builder.Append(c - 55);
            else
                builder.Append(c);
        }

        // this part is necessary because the number is too much long. divide in chunck
        string numeric = builder.ToString();
        int remainder = 0;
        int i = 0;
        while (i < numeric.Length)
        {
            // max 7 chars at time
            string chunk = remainder.ToString() + numeric.Substring(i, Math.Min(7, numeric.Length - i));
            remainder = int.Parse(chunk) % 97;
            i += 7;
        }

        return remainder == 1;
    }
}
