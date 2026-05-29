namespace CodiceFiscale.Tests;

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
    [InlineData("")]          // Empty string
    [InlineData("   ")]       // Whitespace only
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
    [InlineData("RSSMRA85B29A562?")] // 29 febbraio anno non bisestile (1985)
    [InlineData("RSSMRA04B69A562?")] // femmina, 29 febbraio anno non bisestile (2004... 2004 è bisestile, usa 1985→85)
    [InlineData("RSSMRA85D31A562?")] // 31 aprile (aprile ha 30 giorni)
    [InlineData("RSSMRA85H31A562?")] // 31 giugno (giugno ha 30 giorni)
    [InlineData("RSSMRA85P31A562?")] // 31 settembre (settembre ha 30 giorni)
    [InlineData("RSSMRA85S31A562?")] // 31 novembre (novembre ha 30 giorni)
    [InlineData("RSSMRA85T72A562?")] // giorno 72 femmina (40+32, non esiste)
    [InlineData("RSSMRA85T00A562?")] // giorno 00, invalido
    [InlineData("RSSMRA85T40A562?")] // giorno 40, né maschio né femmina (buco tra 31 e 41)
    public void CheckInvalidDay_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S\t")]  // tab in coda
    [InlineData("\nRSSMRA85T10A562S")]  // newline in testa
    [InlineData("RSSMRA85T10A562S ")]  // spazio in coda
    [InlineData(" RSSMRA85T10A562S")]  // spazio in testa
    [InlineData("RSSMRA85T10A562 S")]  // spazio nel mezzo
    public void CheckCFWithWhitespace_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562$")]  // carattere speciale come check char
    [InlineData("RSS1RA85T10A562S")]  // cifra al posto di lettera nel cognome
    [InlineData("RSSMRA8XT10A562S")]  // lettera al posto di cifra nell'anno
    [InlineData("RSSMRA85110A562S")]  // cifra al posto della lettera mese
    [InlineData("RSSMRA85TAA562S1")]  // lettere al posto del giorno
    public void CheckCFWithWrongCharacterTypes_ShouldBeFalse(string cf)
    {
        Assert.False(CodiceFiscaleValidator.IsValid(cf));
    }

    [Theory]
    [InlineData("RSSMRA85T10A562S")] // stesso CF due volte di fila, verifica no side effects
    public void CheckValidCF_CalledTwice_ShouldBeConsistent(string cf)
    {
        Assert.True(CodiceFiscaleValidator.IsValid(cf));
        Assert.True(CodiceFiscaleValidator.IsValid(cf));
    }
}