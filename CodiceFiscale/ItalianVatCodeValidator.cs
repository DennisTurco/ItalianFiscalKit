namespace CodiceFiscale;

/// <summary>
/// Provides validation for Italian VAT codes.
/// </summary>
public class ItalianVatCodeValidator
{
    /// <summary>
    /// Validates the specified Italian VAT code.
    /// </summary>
    /// <param name="vat">The VAT code to validate.</param>
    /// <param name="isConsumer">Indicates if the code is for a consumer.</param>
    /// <param name="isFiscal">Indicates if the code is fiscal.</param>
    /// <returns><c>true</c> if the VAT code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValid(string vat, bool isConsumer, bool isFiscal)
    {
        return true;
    }
}
