using System.Text;
using System.Text.RegularExpressions;
using ItalianFiscalKit.Utils;

namespace ItalianFiscalKit;

/// <summary>
/// Provides validation for Italian VAT codes (Partita IVA) and fiscal codes used in a VAT context.
/// </summary>
public class ItalianVatCodeValidator
{
    private const string VatRegexPattern = @"^(\d{7})(\d{3})(\d{1})$";

    /// <summary>
    /// Validates the specified Italian VAT or fiscal code string.
    /// </summary>
    /// <param name="vat">The code to validate. Can be an 11-digit Partita IVA or, if <paramref name="isFiscal"/> is <c>true</c>, a 16-character Fiscal Code.</param>
    /// <param name="isConsumer">
    /// <c>true</c> if the code belongs to a natural person (consumer); the first digit must be 8 or 9.
    /// <c>false</c> for standard company VAT codes (first digit 0–7).
    /// </param>
    /// <param name="isFiscal">
    /// <c>true</c> to also accept a 16-character Fiscal Code as valid input in addition to an 11-digit VAT code.
    /// <c>false</c> to accept only the 11-digit Partita IVA format.
    /// </param>
    /// <returns><c>true</c> if the code is valid according to the specified rules; otherwise, <c>false</c>.</returns>
    public static bool IsValid(string vat, bool isConsumer, bool isFiscal)
    {
        if (string.IsNullOrEmpty(vat))
            return false;

        if (isFiscal && vat.Length == 16)
            return FiscalCodeValidator.IsValid(vat);

        if (vat.Length != 11)
            return false;

        vat = vat.Trim().Replace(" ", "");

        if (!Regex.IsMatch(vat, VatRegexPattern))
            return false;

        int firstDigit = int.Parse(vat[0].ToString());
        if (isConsumer && (firstDigit != 8 && firstDigit != 9))
            return false;
        if (!isConsumer && firstDigit > 7)
            return false;

        Match match = Regex.Match(vat, VatRegexPattern);
        string serialNumber = match.Groups[1].Value;
        string postalCode = match.Groups[2].Value;
        string checkCode = match.Groups[3].Value;

        return
            IsPostalCodeValid(postalCode) &&
            LuhnAlgorithm(serialNumber + postalCode) == int.Parse(checkCode);
    }

    private static bool IsPostalCodeValid(string postalCode)
        => DataStore.Municipalities.Any(x => x.Province.Code == postalCode);

    // to calculate the check code it is necessary calculate it using the Luhn algorithm
    private static int LuhnAlgorithm(string value)
    {
        StringBuilder builder = new();

        bool even = value.Length % 2 == 0;

        for (int i = 0; i < value.Length; i++)
        {
            int v = int.Parse(value[i].ToString());
            if (even && i % 2 == 1)
            {
                v *= 2;
                if (v > 9)
                {
                    string vString = v.ToString();
                    v = int.Parse(vString[0].ToString()) + int.Parse(vString[1].ToString());
                }
            }
            builder.Append(v);
        }

        int totalSum = builder.ToString().Sum(x => int.Parse(x.ToString()));
        int rest = totalSum % 10;
        return rest == 0 ? rest : 10 - rest;
    }
}
