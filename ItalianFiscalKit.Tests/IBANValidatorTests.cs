namespace ItalianFiscalKit.Tests;

public class IBANValidatorTests
{
    [Theory]
    [InlineData("IT60X0542811101000000123456")]        // italy
    [InlineData("DE89370400440532013000")]             // germany
    [InlineData("GB29NWBK60161331926819")]             // UK
    [InlineData("FR7630006000011234567890189")]        // france
    [InlineData("ES9121000418450200051332")]           // spain
    [InlineData("CH5604835012345678009")]              // switzerland
    [InlineData("NO9386011117947")]                   // norway
    [InlineData("IT60 X054 2811 1010 0000 0123 456")] // italy with spaces
    public void CheckIBAN_ShouldReturnCorrectIBAN(string iban)
    {
        Assert.True(IBANValidator.IsValid(iban));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CheckNullIBAN_ShouldReturnInvalidIBAN(string iban)
    {
        Assert.False(IBANValidator.IsValid(iban));
    }

    [Theory]
    [InlineData("IT00X0542811101000000123456")]  // wrong checksum
    [InlineData("XX60X0542811101000000123456")]  // non-SEPA country
    [InlineData("US60X0542811101000000123456")]  // non-SEPA country
    [InlineData("IT60X054281110100000012345")]   // wrong length (26 instead of 27)
    [InlineData("IT60X05428111010000001234567")] // wrong length (28 instead of 27)
    [InlineData("ITXX")]                         // too short
    public void CheckInvalidIBAN_ShouldReturnInvalidIBAN(string iban)
    {
        Assert.False(IBANValidator.IsValid(iban));
    }
}
