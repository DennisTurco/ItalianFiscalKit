namespace ItalianFiscalKit.Tests;

public class CodiceFiscaleValidatorTests
{
    [Theory]
    [InlineData("RSSMRA85T10A562S")] // Mario Rossi, male
    [InlineData("BNCGNN90A01H501V")] // Giovanna Bianchi, male day
    [InlineData("VRDLGU70E30F839C")] // Luigi Verdi, male
    [InlineData("MRNGRL01P55Z614X")] // Gabriella Morin, female, born abroad
    [InlineData("GLLMTT99C06G479M")] // Matteo Galli, male
    public void CheckValidCF_ShouldBeTrue(string cf)
    {
        Assert.True(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562X")] // Wrong check character
    [InlineData("RSSMRA85T10A562")]  // Too short (15 chars)
    [InlineData("RSSMRA85T10A562SS")] // Too long (17 chars)
    [InlineData("RSSMRA85T32A562S")] // Invalid day (32)
    [InlineData("RSSMRA85T10Z999S")] // Non-existent municipality code
    public void CheckInvalidCF_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("")] // Empty string
    [InlineData("   ")] // Whitespace only
    [InlineData("123456789012345")] // All digits, no letters
    [InlineData("AAAAAA00A00A000A")] // All placeholder characters
    public void CheckNullOrMalformedCF_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("rssmra85t10a562s")] // Lowercase valid CF
    [InlineData("RsSmRa85T10a562S")] // Mixed case valid CF
    public void CheckValidCF_CaseInsensitive_ShouldBeTrue(string cf)
    {
        Assert.True(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T71A562X")] // Female day: 71 (40+31, valid for October)
    [InlineData("BNCGNN90A41H501Z")] // Female day: 41 (40+01, valid for January)
    public void CheckFemaleCF_ShouldBeTrue(string cf)
    {
        Assert.True(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85B29A562X")] // February 29, 1985 (non-leap year)
    [InlineData("RSSMRA04B69A562X")] // female, February 29 in a non-leap year (2004... 2004 is a leap year, use 1985->85)
    [InlineData("RSSMRA85D31A562X")] // April 31 (April has 30 days)
    [InlineData("RSSMRA85H31A562X")] // June 31 (has 30 days)
    [InlineData("RSSMRA85P31A562X")] // June 31 (has 30 days)
    [InlineData("RSSMRA85S31A562X")] // November 31 (November has 30 days)
    [InlineData("RSSMRA85T72A562X")] // Day 72, female (40+32, does not exist)
    [InlineData("RSSMRA85T00A562X")] // Day 00, invalid
    [InlineData("RSSMRA85T40A562X")] // Day 40, neither male nor female (gap between 31 and 41)
    public void CheckInvalidDay_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S\t")]
    [InlineData("\nRSSMRA85T10A562S")]
    [InlineData("RSSMRA85T10A562S ")]
    [InlineData(" RSSMRA85T10A562S")]
    [InlineData("RSSMRA85T10A562 S")]
    public void CheckCFWithWhitespace_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562$")]
    [InlineData("RSS1RA85T10A562S")]
    [InlineData("RSSMRA8XT10A562S")]
    [InlineData("RSSMRA85110A562S")]
    [InlineData("RSSMRA85TAA562S1")]
    public void CheckCFWithWrongCharacterTypes_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S")] // check for side effects
    public void CheckValidCF_CalledTwice_ShouldBeConsistent(string cf)
    {
        Assert.True(CodiceFiscaleValidator.IsValid(cf));
        Assert.True(CodiceFiscaleValidator.IsValid(cf));
    }
}