namespace ItalianFiscalKit.Tests;

public class ItalianVatCodeValidatorTests
{
    [Theory]
    [InlineData("00484960588", false, false)]  // RAI SpA - Roma (058)
    [InlineData("10432980588", false, false)]  // Roma (058)
    [InlineData("38637940263", false, false)]  // Venezia (026)
    [InlineData("71234560580", false, false)]  // Roma (058)
    public void CheckVAT_ShouldReturnTrue_ForValidCompanyVAT(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.True(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("85432100585", true, false)]  // Roma (058)
    [InlineData("91234560588", true, false)]  // Roma (058)
    public void CheckVAT_ShouldReturnTrue_ForValidConsumerVAT(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.True(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S", false, true)]
    [InlineData("BNCSFN91P65F205Z", false, true)]
    [InlineData("00484960588", false, true)]
    public void CheckVAT_ShouldReturnTrue_ForFiscalFlag(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.True(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Fact]
    public void CheckVAT_ShouldReturnFalse_ForNull()
    {
        Assert.False(ItalianVatCodeValidator.IsValid(null!, false, false));
    }

    [Theory]
    [InlineData("", false, false)]
    [InlineData("  ", false, false)]
    public void CheckVAT_ShouldReturnFalse_ForEmpty(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("0218208039", false, false)]
    [InlineData("021820803900", false, false)]
    [InlineData("00484960588 ", false, false)]
    public void CheckVAT_ShouldReturnFalse_ForWrongLength(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("ABC12345678", false, false)]
    [InlineData("0048496058A", false, false)]
    [InlineData("00484 96058", false, false)]
    public void CheckVAT_ShouldReturnFalse_ForNonNumeric(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("02182080391", false, false)]
    [InlineData("00484960580", false, false)]
    public void CheckVAT_ShouldReturnFalse_ForBadChecksum(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("00000000000", false, false)]  // province "000" does not exist; checksum is valid
    public void CheckVAT_ShouldReturnFalse_ForInvalidProvinceCode(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("84321000584", false, false)]
    [InlineData("91234560158", false, false)]
    [InlineData("71234560580", true, false)]
    [InlineData("00484960588", true, false)]
    public void CheckVAT_ShouldReturnFalse_ForConsumerMismatch(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S", false, false)]
    public void CheckVAT_ShouldReturnFalse_ForCFWithoutFiscalFlag(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562X", false, true)]
    [InlineData("XXXXXXXXXXXXXXXX", false, true)]
    public void CheckVAT_ShouldReturnFalse_ForInvalidCFWithFiscalFlag(string vat, bool isConsumer, bool isFiscal)
    {
        Assert.False(ItalianVatCodeValidator.IsValid(vat, isConsumer, isFiscal));
    }
}
