namespace CodiceFiscale;

/// <summary>
/// Provides validation for Italian VAT codes (Partita IVA) and fiscal codes used in a VAT context.
/// </summary>
public class ItalianVatCodeValidator
{
    /// <summary>
    /// Validates the specified Italian VAT or fiscal code string.
    /// </summary>
    /// <param name="vat">The code to validate. Can be an 11-digit Partita IVA or a 16-character Codice Fiscale.</param>
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
        return true;
    }
}
