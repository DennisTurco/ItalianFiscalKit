namespace CodiceFiscale.Tests;

public class IBANValidatorTests
{
    [Theory]
    [InlineData("IT60X0542811101000000123456")]   // italy
    [InlineData("DE89370400440532013000")]        // germany
    [InlineData("GB29NWBK60161331926819")]        // UK
    [InlineData("FR7630006000011234567890189")]   // france
    [InlineData("ES9121000418450200051332")]      // spain
    [InlineData("IT60 X054 2811 1010 0000 0123 456")] // italy with spaces
    public void CheckIBAN_ShouldReturnCorrectIBAN(string iban)
    {
        Assert.True(IBANValidator.IsValid(iban));
    }

    [Theory]
    [InlineData("")]
    public void CheckNullIBAN_ShouldReturnInvalidIBAN(string iban)
    {
        Assert.False(IBANValidator.IsValid(iban));
    }

    [Theory]
    [InlineData("IT00X0542811101000000123456")]  // checksum error
    [InlineData("XX60X0542811101000000123456")]  // cauntry error
    [InlineData("IT60X054281110100000012345")]   // invalid lenght
    public void CheckInvalidIBAN_ShouldReturnInvalidIBAN(string iban)
    {
        Assert.False(IBANValidator.IsValid(iban));
    }
}