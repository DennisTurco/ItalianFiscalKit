using ItalianFiscalKit.Entities;
using ItalianFiscalKit.Enums;
using ItalianFiscalKit.Exceptions;

namespace ItalianFiscalKit.Tests;

public class FiscalCodeParserTests
{
    [Fact]
    public void ParseValidMaleCF_ShouldReturnCorrectData()
    {
        FiscalCodeData result = FiscalCodeParser.Parse("VRDLGU70E30F839C");
        Assert.Equal(new(Gender.Male, new DateOnly(1970, 5, 30), "F839"), result);
    }

    [Fact]
    public void TryParseValidMaleCF_ShouldReturnCorrectData()
    {
        bool success = FiscalCodeParser.TryParse("VRDLGU70E30F839C", out FiscalCodeData? result);
        Assert.True(success);
        Assert.NotNull(result);
        Assert.Equal(new FiscalCodeData(Gender.Male, new DateOnly(1970, 5, 30), "F839"), result);
    }

    [Fact]
    public void ParseValidFemaleCF_ShouldReturnCorrectData()
    {
        FiscalCodeData result = FiscalCodeParser.Parse("MRNGRL01P55Z614X");
        Assert.Equal(new(Gender.Female, new DateOnly(2001, 9, 15), "Z614"), result);
    }

    [Fact]
    public void TryParseValidFemaleCF_ShouldReturnCorrectData()
    {
        bool success = FiscalCodeParser.TryParse("MRNGRL01P55Z614X", out FiscalCodeData? result);
        Assert.True(success);
        Assert.NotNull(result);
        Assert.Equal(new(Gender.Female, new DateOnly(2001, 9, 15), "Z614"), result);
    }

    [Fact]
    public void ParseValidCF_BornAbroad_ShouldReturnForeignCode()
    {
        FiscalCodeData result = FiscalCodeParser.Parse("MRNGRL01P55Z614X");
        Assert.StartsWith("Z", result.BelfioreCode);
    }

    [Fact]
    public void TryParseValidCF_BornAbroad_ShouldReturnForeignCode()
    {
        bool success = FiscalCodeParser.TryParse("MRNGRL01P55Z614X", out FiscalCodeData? result);
        Assert.True(success);
        Assert.NotNull(result);
        Assert.StartsWith("Z", result.BelfioreCode);
    }

    [Fact]
    public void ParseValidCF_Lowercase_ShouldWork()
    {
        FiscalCodeData result = FiscalCodeParser.Parse("vrdlgu70e30f839c");
        FiscalCodeData expected = new FiscalCodeData(Gender.Male, new DateOnly(1970, 5, 30), "F839");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryParseValidCF_Lowercase_ShouldWork()
    {
        bool success = FiscalCodeParser.TryParse("vrdlgu70e30f839c", out FiscalCodeData? result);
        Assert.True(success);
        Assert.NotNull(result);
        Assert.Equal(new FiscalCodeData(Gender.Male, new DateOnly(1970, 5, 30), "F839"), result);
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S", 1985, 12, 10, Gender.Male)]
    [InlineData("VRDLGU70E30F839C", 1970, 5, 30, Gender.Male)]
    [InlineData("MRNGRL01P55Z614X", 2001, 9, 15, Gender.Female)]
    public void ParseValidCF_ShouldReturnCorrectDateAndGender(string cf, int year, int month, int day, Gender gender)
    {
        FiscalCodeData result = FiscalCodeParser.Parse(cf);
        Assert.Equal(gender, result.Gender);
        Assert.Equal(new DateOnly(year, month, day), result.DateOfBirth);
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S", 1985, 12, 10, Gender.Male)]
    [InlineData("VRDLGU70E30F839C", 1970, 5, 30, Gender.Male)]
    [InlineData("MRNGRL01P55Z614X", 2001, 9, 15, Gender.Female)]
    public void TryParseValidCF_ShouldReturnCorrectDateAndGender(string cf, int year, int month, int day, Gender gender)
    {
        bool success = FiscalCodeParser.TryParse(cf, out FiscalCodeData? result);
        Assert.True(success);
        Assert.NotNull(result);
        Assert.Equal(gender, result.Gender);
        Assert.Equal(new DateOnly(year, month, day), result.DateOfBirth);
    }

    [Fact]
    public void ParseInvalidCF_WrongCheckChar_ShouldThrowException()
    {
        Assert.Throws<InvalidFiscalCodeException>(() => FiscalCodeParser.Parse("VRDLGU70E30F839X"));
    }

    [Fact]
    public void ParseInvalidCF_NonExistentMunicipality_ShouldThrowException()
    {
        Assert.Throws<InvalidFiscalCodeException>(() => FiscalCodeParser.Parse("RSSMRA85T10Z999S"));
    }

    [Fact]
    public void ParseInvalidCF_TooShort_ShouldThrowException()
    {
        Assert.Throws<InvalidFiscalCodeException>(() => FiscalCodeParser.Parse("VRDLGU70E30F839"));
    }

    [Fact]
    public void ParseEmptyCF_ShouldThrowException()
    {
        Assert.Throws<InvalidFiscalCodeException>(() => FiscalCodeParser.Parse(""));
    }
}
