# Getting Started

## 1. Installation

```bash
dotnet add package ItalianFiscalKit
```

Or search for **ItalianFiscalKit** in the NuGet Package Manager inside Visual Studio.

## 2. Validate a Fiscal Code

Pass any string — valid or not — and get back a plain `bool`. The method accepts lowercase input, handles all edge cases, and never throws.

```csharp
using ItalianFiscalKit;

bool valid = FiscalCodeValidator.IsValid("RSSMRA85T10A562S"); // true

// lowercase is fine
bool valid2 = FiscalCodeValidator.IsValid("rssmra85t10a562s"); // true

// foreign birthplace (Z-code)
bool validForeign = FiscalCodeValidator.IsValid("MRNGRL01P55Z614X"); // true

// wrong check digit
bool invalid = FiscalCodeValidator.IsValid("RSSMRA85T10A562X"); // false
```

`IsValid` returns `false` for anything invalid: `null`, wrong length, bad characters, impossible date, unknown Belfiore code, wrong check digit.

## 3. Parse a Fiscal Code

Use `TryParse` whenever the input comes from outside your app — it''s the safest option because it never throws.

```csharp
using ItalianFiscalKit;
using ItalianFiscalKit.Entities;
using ItalianFiscalKit.Enums;

FiscalCodeData data = FiscalCodeParser.Parse("RSSMRA85T10A562S");

Gender gender   = data.Gender;       // Gender.Male
DateOnly dob    = data.DateOfBirth;  // new DateOnly(1985, 12, 10)
string belfiore = data.BelfioreCode; // "A562"
```

> [!WARNING]
> `Parse` throws `InvalidFiscalCodeException` if the input is invalid.
> Prefer `TryParse` to avoid try/catch:

```csharp
if (FiscalCodeParser.TryParse(userInput, out FiscalCodeData? data))
{
    Console.WriteLine($"Born: {data.DateOfBirth}, Gender: {data.Gender}");
}
```

## 4. Generate a Fiscal Code

Provide the personal data and the Belfiore code of the birthplace, and you get back a fully valid CF.

```csharp
using ItalianFiscalKit;
using ItalianFiscalKit.Enums;

string cf = FiscalCodeGenerator.Generate(
    name:         "Luigi",
    surname:      "Verdi",
    dateOfBirth:  new DateOnly(1970, 5, 30),
    gender:       Gender.Male,
    belfioreCode: "F839"
);
// "VRDLGU70E30F839C"
```

`Generate` throws `InvalidFiscalCodeDataException` if the name or surname is shorter than 3 characters, or if the Belfiore code doesn''t exist in the embedded dataset. The generated CF always passes `FiscalCodeValidator.IsValid`.

## 5. Validate an IBAN

Spaces in the input are ignored, so both compact and formatted IBANs work.

```csharp
using ItalianFiscalKit;

bool valid        = IBANValidator.IsValid("IT60X0542811101000000123456");          // true
bool validSpaced  = IBANValidator.IsValid("IT60 X054 2811 1010 0000 0123 456");    // true
bool invalid      = IBANValidator.IsValid("IT00X0542811101000000123456");          // false
```

## 6. Validate a Partita IVA

```csharp
using ItalianFiscalKit;

// standard company VAT
bool valid    = ItalianVatCodeValidator.IsValid("00484960588", isConsumer: false, isFiscal: false); // true

// natural person (first digit must be 8 or 9)
bool consumer = ItalianVatCodeValidator.IsValid("85423511618", isConsumer: true,  isFiscal: false); // true

// also accept a Fiscal Code as fiscal identifier
bool fiscal   = ItalianVatCodeValidator.IsValid("RSSMRA85T10A562S", isConsumer: false, isFiscal: true); // true
```
