namespace CodiceFiscale;

/// <summary>
/// Provides validation for Italian VAT codes (Partita IVA) and fiscal codes used in a VAT context.
/// </summary>
public class ItalianVatCodeValidator
{
    /// <summary>
    /// Validates the specified Italian VAT or fiscal code string.
    /// </summary>
    /// <param name="vat">The code to validate. Can be an 11-digit Partita IVA or, if <paramref name="isFiscal"/> is <c>true</c>, a 16-character Codice Fiscale.</param>
    /// <param name="isConsumer">
    /// <c>true</c> if the code belongs to a natural person (consumer); the first digit must be 8 or 9.
    /// <c>false</c> for standard company VAT codes (first digit 0–7).
    /// </param>
    /// <param name="isFiscal">
    /// <c>true</c> to also accept a 16-character Codice Fiscale as valid input in addition to an 11-digit VAT code.
    /// <c>false</c> to accept only the 11-digit Partita IVA format.
    /// </param>
    /// <returns><c>true</c> if the code is valid according to the specified rules; otherwise, <c>false</c>.</returns>
    public static bool IsValid(string vat, bool isConsumer, bool isFiscal)
    {
        if (string.IsNullOrWhiteSpace(vat))
            return false;

        vat = vat.Trim().ToUpperInvariant();

        // If isFiscal is enabled and the input looks like a CF, delegate to CodiceFiscaleValidator
        if (isFiscal && vat.Length == 16)
            return CodiceFiscaleValidator.IsValid(vat);

        // Must be exactly 11 numeric digits
        if (vat.Length != 11 || !vat.All(char.IsDigit))
            return false;

        // Enforce consumer vs company based on first digit (8 or 9 = consumer)
        int firstDigit = vat[0] - '0';
        if (isConsumer && firstDigit < 8) return false;
        if (!isConsumer && firstDigit >= 8) return false;

        // Luhn-like checksum over the first 10 digits
        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            int digit = vat[i] - '0';
            if (i % 2 == 0) // odd position (1-indexed): add as-is
            {
                sum += digit;
            }
            else            // even position: double, subtract 9 if >= 10
            {
                int doubled = digit * 2;
                sum += doubled >= 10 ? doubled - 9 : doubled;
            }
        }

        int expectedCheck = (10 - sum % 10) % 10;
        return vat[10] - '0' == expectedCheck;
    }
}
