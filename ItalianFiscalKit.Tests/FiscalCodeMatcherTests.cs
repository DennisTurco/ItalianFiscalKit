using ItalianFiscalKit.Enums;
using ItalianFiscalKit.Exceptions;

namespace ItalianFiscalKit.Tests;

public class FiscalCodeMatcherTests
{
    [Fact]
    public void Matches_ValidMaleData_ShouldReturnTrue()
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.True(result);
    }

    [Fact]
    public void Matches_ValidFemaleData_ShouldReturnTrue()
    {
        bool result = FiscalCodeMatcher.Matches(
            "RSSMRA85C55H501V", "Maria", "Rossi", new DateOnly(1985, 3, 15), Gender.Female, "H501");
        Assert.True(result);
    }

    [Fact]
    public void Matches_LowercaseCF_ShouldReturnTrue()
    {
        bool result = FiscalCodeMatcher.Matches(
            "vrdlgu70e30f839c", "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.True(result);
    }

    [Fact]
    public void Matches_CFWithLeadingTrailingSpaces_ShouldReturnTrue()
    {
        bool result = FiscalCodeMatcher.Matches(
            "  VRDLGU70E30F839C  ", "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.True(result);
    }

    [Fact]
    public void Matches_WrongName_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Mario", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.False(result);
    }

    [Fact]
    public void Matches_WrongSurname_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Luigi", "Rossi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.False(result);
    }

    [Fact]
    public void Matches_WrongDateOfBirth_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Luigi", "Verdi", new DateOnly(1971, 5, 30), Gender.Male, "F839");
        Assert.False(result);
    }

    [Fact]
    public void Matches_WrongGender_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Female, "F839");
        Assert.False(result);
    }

    [Fact]
    public void Matches_WrongBelfioreCode_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "H501");
        Assert.False(result);
    }

    [Fact]
    public void Matches_NullCF_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            null!, "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.False(result);
    }

    [Fact]
    public void Matches_EmptyCF_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            "", "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.False(result);
    }

    [Fact]
    public void Matches_InvalidBelfioreCode_ShouldReturnFalse()
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "XXXX");
        Assert.False(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("AB")]
    public void Matches_InvalidName_ShouldReturnFalse(string? name)
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", name!, "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.False(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("AB")]
    public void Matches_InvalidSurname_ShouldReturnFalse(string? surname)
    {
        bool result = FiscalCodeMatcher.Matches(
            "VRDLGU70E30F839C", "Luigi", surname!, new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.False(result);
    }

    [Fact]
    public void Matches_GeneratedCF_ShouldAlwaysReturnTrue()
    {
        string cf = FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        bool result = FiscalCodeMatcher.Matches(cf, "Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.True(result);
    }

    [Fact]
    public void Matches_GeneratedFemaleCF_ShouldAlwaysReturnTrue()
    {
        string cf = FiscalCodeGenerator.Generate("Maria", "Rossi", new DateOnly(1985, 3, 15), Gender.Female, "H501");
        bool result = FiscalCodeMatcher.Matches(cf, "Maria", "Rossi", new DateOnly(1985, 3, 15), Gender.Female, "H501");
        Assert.True(result);
    }
}
