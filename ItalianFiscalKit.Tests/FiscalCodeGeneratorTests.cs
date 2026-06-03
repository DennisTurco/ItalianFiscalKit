using ItalianFiscalKit.Entities;
using ItalianFiscalKit.Enums;
using ItalianFiscalKit.Exceptions;

namespace ItalianFiscalKit.Tests;

public class FiscalCodeGeneratorTests
{
    [Fact]
    public void Generate_ValidMaleData_ShouldReturnCorrectCF()
    {
        string result = FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.Equal("VRDLGU70E30F839C", result);
    }

    [Fact]
    public void Generate_ValidFemaleData_ShouldReturnCorrectCF()
    {
        string result = FiscalCodeGenerator.Generate("Maria", "Rossi", new DateOnly(1985, 3, 15), Gender.Female, "H501");
        Assert.Equal("RSSMRA85C55H501V", result);
    }

    [Fact]
    public void Generate_ValidData_ShouldReturnCFOfLength16()
    {
        string result = FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.Equal(16, result.Length);
    }

    [Fact]
    public void Generate_ValidData_ResultShouldPassValidation()
    {
        string result = FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        Assert.True(FiscalCodeValidator.IsValid(result));
    }

    [Fact]
    public void Generate_MaleGender_DayShouldBeLessOrEqualTo31()
    {
        string result = FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
        int day = int.Parse(result.Substring(9, 2));

        Assert.InRange(day, 1, 31);
    }

    [Fact]
    public void Generate_FemaleGender_DayShouldBeGreaterThan40()
    {
        string result = FiscalCodeGenerator.Generate("Maria", "Rossi", new DateOnly(1985, 3, 15), Gender.Female, "H501");
        int day = int.Parse(result.Substring(9, 2));

        Assert.InRange(day, 41, 71);
    }

    [Fact]
    public void Generate_NullName_ShouldThrowInvalidFiscalCodeDataException()
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate(null!, "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839"));
    }

    [Fact]
    public void Generate_EmptyName_ShouldThrowInvalidFiscalCodeDataException()
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate("", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839"));
    }

    [Theory]
    [InlineData("A")]
    [InlineData("AB")]
    public void Generate_NameShorterThan3Chars_ShouldThrowInvalidFiscalCodeDataException(string shortName)
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate(shortName, "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839"));
    }

    [Fact]
    public void Generate_NullSurname_ShouldThrowInvalidFiscalCodeDataException()
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate("Luigi", null!, new DateOnly(1970, 5, 30), Gender.Male, "F839"));
    }

    [Fact]
    public void Generate_EmptySurname_ShouldThrowInvalidFiscalCodeDataException()
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate("Luigi", "", new DateOnly(1970, 5, 30), Gender.Male, "F839"));
    }

    [Theory]
    [InlineData("A")]
    [InlineData("AB")]
    public void Generate_SurnameShorterThan3Chars_ShouldThrowInvalidFiscalCodeDataException(string shortSurname)
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate("Luigi", shortSurname, new DateOnly(1970, 5, 30), Gender.Male, "F839"));
    }

    [Fact]
    public void Generate_NullBelfiore_ShouldThrowInvalidFiscalCodeDataException()
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("F83")]
    [InlineData("F8399")]
    public void Generate_BelfioreWithWrongLength_ShouldThrowInvalidFiscalCodeDataException(string belfiore)
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, belfiore));
    }

    [Theory]
    [InlineData("0000")]
    [InlineData("AAAA")]
    public void Generate_InvalidBelfioreFormat_ShouldThrowInvalidFiscalCodeDataException(string belfiore)
    {
        Assert.Throws<InvalidFiscalCodeDataException>(() =>
            FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, belfiore));
    }

    [Fact]
    public void Generate_ThenParse_ShouldReturnOriginalData()
    {
        Gender gender = Gender.Male;
        DateOnly dateOfBirth = new(1970, 5, 30);
        string belfiore = "F839";

        string cf = FiscalCodeGenerator.Generate("Luigi", "Verdi", dateOfBirth, gender, belfiore);
        bool success = FiscalCodeParser.TryParse(cf, out FiscalCodeData? parsed);

        Assert.True(success);
        Assert.NotNull(parsed);
        Assert.Equal(gender, parsed.Gender);
        Assert.Equal(dateOfBirth, parsed.DateOfBirth);
        Assert.Equal(belfiore, parsed.BelfioreCode);
    }
}
