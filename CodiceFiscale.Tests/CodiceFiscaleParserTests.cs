using CodiceFiscale.Entities;
using CodiceFiscale.Enums;
using CodiceFiscale.Exceptions;

namespace CodiceFiscale.Tests;

public class CodiceFiscaleParserTests
{
    [Fact]
    public void ParseValidMaleCF_ShouldReturnCorrectData()
    {
        CodiceFiscaleData result = CodiceFiscaleParser.Parse("VRDLGU70E30F839C");
        CodiceFiscaleData expected = new(Gender.Male, new DateOnly(1970, 5, 30), "F839");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseValidFemaleCF_ShouldReturnCorrectData()
    {
        CodiceFiscaleData result = CodiceFiscaleParser.Parse("MRNGRL01P55Z614X");
        CodiceFiscaleData expected = new (Gender.Female, new DateOnly(2001, 9, 15), "Z614");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseValidCF_BornAbroad_ShouldReturnForeignCode()
    {
        CodiceFiscaleData result = CodiceFiscaleParser.Parse("MRNGRL01P55Z614X");
        Assert.StartsWith("Z", result.BelfioreCode);
    }

    [Fact]
    public void ParseValidCF_Lowercase_ShouldWork()
    {
        CodiceFiscaleData result = CodiceFiscaleParser.Parse("vrdlgu70e30f839c");
        CodiceFiscaleData expected = new CodiceFiscaleData(Gender.Male, new DateOnly(1970, 5, 30), "F839");
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S", 1985, 12, 10, Gender.Male)]
    [InlineData("VRDLGU70E30F839C", 1970,  5, 30, Gender.Male)]
    [InlineData("MRNGRL01P55Z614X", 2001,  9, 15, Gender.Female)]
    public void ParseValidCF_ShouldReturnCorrectDateAndGender(string cf, int year, int month, int day, Gender gender)
    {
        CodiceFiscaleData result = CodiceFiscaleParser.Parse(cf);
        Assert.Equal(gender, result.Gender);
        Assert.Equal(new DateOnly(year, month, day), result.DateOfBirth);
    }

    [Fact]
    public void ParseInvalidCF_WrongCheckChar_ShouldThrowException()
    {
        Assert.Throws<InvalidCodiceFiscaleException>(() => CodiceFiscaleParser.Parse("VRDLGU70E30F839X"));
    }

    [Fact]
    public void ParseInvalidCF_NonExistentMunicipality_ShouldThrowException()
    {
        Assert.Throws<InvalidCodiceFiscaleException>(() => CodiceFiscaleParser.Parse("RSSMRA85T10Z999S"));
    }

    [Fact]
    public void ParseInvalidCF_TooShort_ShouldThrowException()
    {
        Assert.Throws<InvalidCodiceFiscaleException>(() => CodiceFiscaleParser.Parse("VRDLGU70E30F839"));
    }

    [Fact]
    public void ParseEmptyCF_ShouldThrowException()
    {
        Assert.Throws<InvalidCodiceFiscaleException>(() => CodiceFiscaleParser.Parse(""));
    }
}