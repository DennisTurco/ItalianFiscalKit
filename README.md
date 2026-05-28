# Codice Fiscale

A lightweight, dependency-free .NET library for Italian fiscal code (codice fiscale), VAT number (partita IVA) and IBAN validation.

All validation logic runs **locally**. No HTTP calls, no external services, no data ever leaves your application.

[![NuGet](https://img.shields.io/nuget/v/CodiceFiscale.svg)](https://www.nuget.org/packages/CodiceFiscale)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Installation

```bash
 dotnet add package CodiceFiscale
```

## Features

- Fiscal code validation (format + checksum)
- Fiscal code generation from personal data
- Cross-check: does this fiscal code match these personal details?
- Italian VAT number validation (standard, public entity, foreign)
- IBAN validation (mod97, all countries)
- Comuni and codici Belfiore embedded dataset (no external files needed)
- Zero dependencies
- Fully tested

## Usage

1. Validate a fiscal code

    ```cs
    using CodiceFiscale;

    bool isValid = CodiceFiscaleValidator.IsValid("RSSMRA80A01H501U"); // true

    Generate a fiscal code

    var data = new PersonalData
    {
        FirstName       = "Mario",
        LastName        = "Rossi",
        Gender          = Gender.Male,
        DateOfBirth     = new DateOnly(1980, 1, 1),
        CityOfBirth     = "Roma"
    };

    string cf = CodiceFiscaleGenerator.Generate(data); // "RSSMRA80A01H501U"
    ```

2. Cross-check fiscal code against personal data

    ```cs
    bool matches = CodiceFiscaleMatcher.Matches(
        fiscalCode: "RSSMRA80A01H501U",
        firstName:  "Mario",
        lastName:   "Rossi",
        gender:     Gender.Male,
        dateOfBirth: new DateOnly(1980, 1, 1),
        cityOfBirth: "Roma"
    ); // true
    ```

3. Parse a fiscal code

    ```cs
    CodiceFiscaleData parsed = CodiceFiscaleParser.Parse("RSSMRA80A01H501U");

    parsed.Gender       // Male
    parsed.DateOfBirth  // 1980-01-01
    parsed.BelfioreCode // "H501"
    parsed.CityOfBirth  // "Roma"
    ```

4. Validate an Italian VAT number

    ```cs
    bool isValid = ItalianVatCodeValidator.IsValid("12345670017"); // true
    ```

5. Validate an IBAN

    ```cs
    bool isValid = IBANValidator.IsValid("IT60X0542811101000000123456"); // true
    ```

## Edge cases handled

- Names with fewer than 3 consonants (Re, Li, Yu)
- Names with apostrophes and accents (D'Amico, Rosà)
- Foreign-born individuals (Belfiore code Z + country number)
- Omocodia (alternate fiscal codes with letters replacing digits)
- VAT numbers for public entities (starting with 8 or 9)
- Obsolete municipalities (comuni soppressi)

## Why not an API?

Fiscal codes and VAT numbers are personal data. Sending them to a third-party
server to validate them raises immediate GDPR concerns. This library validates
everything locally. The data never leaves your application.

## Dataset

The comuni/Belfiore dataset is sourced from
ISTAT open data and embedded directly
in the library binary. No external files are required at runtime.

The dataset is updated with each minor release to reflect municipality changes
(merges, renames, new comuni).

## Contributing

Contributions are welcome. If you find a fiscal code that validates incorrectly,
please open an issue with:

1. The fiscal code (you can anonymise it — just keep the structure)
2. The personal data it should/should not match
3. The expected result

## License

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Build

```powershell
dotnet build
dotnet test
```
