# Getting Started

## Installation

```bash
dotnet add package CodiceFiscale
```

Or search for **CodiceFiscale** in the NuGet Package Manager inside Visual Studio.

---

## Validate a Codice Fiscale

Pass any string — valid or not — and get back a plain `bool`. The method accepts lowercase input, handles all edge cases, and never throws.

```csharp
using ItalianFiscalKit;

bool valid = CodiceFiscaleValidator.IsValid("RSSMRA85T10A562S");
// true

// lowercase is fine
bool valid2 = CodiceFiscaleValidator.IsValid("rssmra85t10a562s");
// true

// foreign birthplace (Z-code)
bool validForeign = CodiceFiscaleValidator.IsValid("MRNGRL01P55Z614X");
// true

// wrong check digit
bool invalid = CodiceFiscaleValidator.IsValid("RSSMRA85T10A562X");
// false
```

`IsValid` returns `false` for anything invalid: `null`, wrong length, bad characters, impossible date, unknown Belfiore code, wrong check digit.

---

## Parse a Codice Fiscale

Use `TryParse` whenever the input comes from outside your app — it''s the safest option because it never throws.

```csharp
using ItalianFiscalKit;
using CodiceFiscale.Entities;
using CodiceFiscale.Enums;

CodiceFiscaleData data = CodiceFiscaleParser.Parse("RSSMRA85T10A562S");

Gender gender   = data.Gender;       // Gender.Male
DateOnly dob    = data.DateOfBirth;  // new DateOnly(1985, 12, 10)
string belfiore = data.BelfioreCode; // "A562"
```

> [!WARNING]
> `Parse` throws `InvalidCodiceFiscaleException` if the input is invalid.
> Prefer `TryParse` to avoid try/catch:

```csharp
if (CodiceFiscaleParser.TryParse(userInput, out CodiceFiscaleData? data))
{
    Console.WriteLine($"Born: {data.DateOfBirth}, Gender: {data.Gender}");
}
```

---

## Generate a Codice Fiscale

Provide the personal data and the Belfiore code of the birthplace, and you get back a fully valid CF.

```csharp
using ItalianFiscalKit;
using CodiceFiscale.Enums;

string cf = CodiceFiscaleGenerator.Generate(
    name:         "Luigi",
    surname:      "Verdi",
    dateOfBirth:  new DateOnly(1970, 5, 30),
    gender:       Gender.Male,
    belfioreCode: "F839"
);
// "VRDLGU70E30F839C"
```

`Generate` throws `InvalidCodiceFiscaleDataException` if the name or surname is shorter than 3 characters, or if the Belfiore code doesn''t exist in the embedded dataset. The generated CF always passes `CodiceFiscaleValidator.IsValid`.

---

## Validate an IBAN

Spaces in the input are ignored, so both compact and formatted IBANs work.

```csharp
using ItalianFiscalKit;

bool valid        = IBANValidator.IsValid("IT60X0542811101000000123456");          // true
bool validSpaced  = IBANValidator.IsValid("IT60 X054 2811 1010 0000 0123 456");    // true
bool invalid      = IBANValidator.IsValid("IT00X0542811101000000123456");           // false
```

---

## Validate a Partita IVA

```csharp
using ItalianFiscalKit;

// standard company VAT
bool valid    = ItalianVatCodeValidator.IsValid("00484960588", isConsumer: false, isFiscal: false); // true

// natural person (first digit must be 8 or 9)
bool consumer = ItalianVatCodeValidator.IsValid("85423511618", isConsumer: true,  isFiscal: false); // true

// also accept a Codice Fiscale as fiscal identifier
bool fiscal   = ItalianVatCodeValidator.IsValid("RSSMRA85T10A562S", isConsumer: false, isFiscal: true); // true
```
