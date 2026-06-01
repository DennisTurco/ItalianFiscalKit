namespace CodiceFiscale.Tests;

public class ItalianVatCodeValidatorTests
{
    [Theory]
    [InlineData("00484960588", false, false)]  // italian vat code
    [InlineData("10433218194", false, false)]  // rome
    [InlineData("38637940263", false, false)]  // naples
    public void CheckVAT_ShouldReturnValidVAT(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.True(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("85423511618", true, false)]
    [InlineData("91849593107", true, false)]
    public void CheckVAT_ShouldReturnValidConsumerVAT(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.True(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S", false, true)] // CF ok
    [InlineData("BNCSFN91P65F205Z", false, true)] // CF ok
    [InlineData("00484960588", false, true)] // vat code ok
    public void CheckVAT_ShouldReturnValidFiscalCode(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.True(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("", false, false)]
    [InlineData("  ", false, false)]
    public void CheckVAT_ShouldReturnFalseForEmpty(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("0218208039", false, false)]
    [InlineData("021820803900", false, false)]
    [InlineData("ABC123", false, false)]
    public void CheckVAT_ShouldReturnFalseForWrongLength(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("02182080391", false, false)]
    [InlineData("00484960580", false, false)]
    public void CheckVAT_ShouldReturnFalseForBadChecksum(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("80025291002", false, false)]
    [InlineData("90025291008", false, false)]
    [InlineData("02182080390", true, false)]
    public void CheckVAT_ShouldReturnFalseForConsumerMismatch(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S", false, false)]
    public void CheckVAT_ShouldReturnFalseForFiscalCodeWithoutFlag(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }
}