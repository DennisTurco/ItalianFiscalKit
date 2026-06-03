namespace ItalianFiscalKit.Tests;

public class MunicipalityExtensionsTests
{
    [Fact]
    public void GetMunicipalityByBelfiore_ExistingCode_ShouldReturnMunicipality()
    {
        var result = "H501".GetMunicipalityByBelfiore();
        Assert.NotNull(result);
        Assert.Equal("Roma", result.Name);
    }

    [Fact]
    public void GetMunicipalityByBelfiore_NonExistingCode_ShouldReturnNull()
    {
        Assert.Null("XXXX".GetMunicipalityByBelfiore());
    }

    [Fact]
    public void GetMunicipalityByBelfiore_LowercaseCode_ShouldReturnMunicipality()
    {
        var result = "h501".GetMunicipalityByBelfiore();
        Assert.NotNull(result);
    }

    [Fact]
    public void GetMunicipalityByName_ExistingName_ShouldReturnMunicipality()
    {
        var result = "Roma".GetMunicipalityByName();
        Assert.NotNull(result);
        Assert.Equal("H501", result.CatastalCode);
    }

    [Fact]
    public void GetMunicipalityByName_LowercaseName_ShouldReturnMunicipality()
    {
        var result = "roma".GetMunicipalityByName();
        Assert.NotNull(result);
        Assert.Equal("H501", result.CatastalCode);
    }

    [Fact]
    public void GetMunicipalityByName_UppercaseName_ShouldReturnMunicipality()
    {
        var result = "MILANO".GetMunicipalityByName();
        Assert.NotNull(result);
        Assert.Equal("F205", result.CatastalCode);
    }

    [Fact]
    public void GetMunicipalityByName_NonExistingName_ShouldReturnNull()
    {
        Assert.Null("NonExistentCityXYZ".GetMunicipalityByName());
    }

    [Fact]
    public void GetMunicipalityByName_NullInput_ShouldReturnNull()
    {
        Assert.Null(((string)null!).GetMunicipalityByName());
    }

    [Fact]
    public void GetMunicipalityByCAP_ExistingCAP_ShouldReturnMunicipality()
    {
        var result = "00186".GetMunicipalityByCAP();
        Assert.NotNull(result);
        Assert.Equal("Roma", result.Name);
    }

    [Fact]
    public void GetMunicipalityByCAP_NonExistingCAP_ShouldReturnNull()
    {
        Assert.Null("99999".GetMunicipalityByCAP());
    }

    [Fact]
    public void GetMunicipalityByCode_ExistingCode_ShouldReturnMunicipality()
    {
        var result = "058091".GetMunicipalityByCode();
        Assert.NotNull(result);
        Assert.Equal("Roma", result.Name);
    }

    [Fact]
    public void GetMunicipalityByCode_NonExistingCode_ShouldReturnNull()
    {
        Assert.Null("000000".GetMunicipalityByCode());
    }

    [Fact]
    public void GetAllByProvince_ExistingProvince_ShouldReturnAllMunicipalities()
    {
        var result = "Roma".GetAllByProvince();
        Assert.NotNull(result);
        Assert.Equal(121, result!.Count());
    }

    [Fact]
    public void GetAllByProvince_CaseInsensitive_ShouldReturnSameCount()
    {
        var upper = "Roma".GetAllByProvince();
        var lower = "roma".GetAllByProvince();
        Assert.Equal(upper!.Count(), lower!.Count());
    }

    [Fact]
    public void GetAllByProvince_NonExistingProvince_ShouldReturnEmpty()
    {
        var result = "NonExistentProvinceXYZ".GetAllByProvince();
        Assert.NotNull(result);
        Assert.Empty(result!);
    }

    [Fact]
    public void GetAllByProvince_NullInput_ShouldReturnNull()
    {
        var result = ((string)null!).GetAllByProvince();
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_ShouldReturnAllMunicipalities()
    {
        var result = MunicipalityExtensions.GetAll();
        Assert.NotNull(result);
        Assert.Equal(7896, result.Count());
    }

    [Fact]
    public void GetAll_ShouldContainRoma()
    {
        var result = MunicipalityExtensions.GetAll();
        Assert.Contains(result, m => m.CatastalCode == "H501");
    }
}
