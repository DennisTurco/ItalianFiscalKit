# Getting Started

## Installation

```bash
dotnet add package CodiceFiscale
```

Or via the NuGet Package Manager in Visual Studio: search for **CodiceFiscale**.

---

## Validate a Codice Fiscale

```csharp
using CodiceFiscale;

bool valid = CodiceFiscaleValidator.IsValid("RSSMRA85T10A562S");
// true

// Case-insensitive
bool valid2 = CodiceFiscaleValidator.IsValid("rssmra85t10a562s");
// true

// Foreign birth place (Z-code)
bool validForeign = CodiceFiscaleValidator.IsValid("MRNGRL01P55Z614X");
// true

// Invalid check digit
bool invalid = CodiceFiscaleValidator.IsValid("RSSMRA85T10A562X");
// false
```

`IsValid` returns `false` (never throws) for any invalid input including `null`-equivalent strings, wrong length, bad characters, invalid date, unknown Belfiore code, or wrong check digit.

---

## Parse a Codice Fiscale

```csharp
using CodiceFiscale;
using CodiceFiscale.Entities;
using CodiceFiscale.Enums;

CodiceFiscaleData data = CodiceFiscaleParser.Parse("RSSMRA85T10A562S");

Gender gender      = data.Gender;       // Gender.Male
DateOnly dob       = data.DateOfBirth;  // new DateOnly(1985, 12, 10)
string belfiore    = data.BelfioreCode; // "A562"
```

> [!WARNING]
> `Parse` throws `InvalidCodiceFiscaleException` if the input is not valid.
> Use `TryParse` to avoid try/catch:

```csharp
if (CodiceFiscaleParser.TryParse(userInput, out CodiceFiscaleData? data))
{
    Console.WriteLine($"Born: {data.DateOfBirth}, Gender: {data.Gender}");
}
```

---

## Generate a Codice Fiscale

```csharp
using CodiceFiscale;
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

`Generate` throws `InvalidCodiceFiscaleDataException` if name/surname are shorter than 3 characters or the Belfiore code is not found in the embedded dataset. The generated CF always passes `CodiceFiscaleValidator.IsValid`.

---

## Validate an IBAN

```csharp
using CodiceFiscale;

bool valid = IBANValidator.IsValid("IT60X0542811101000000123456"); // true
bool validSpaced = IBANValidator.IsValid("IT60 X054 2811 1010 0000 0123 456"); // true (spaces ignored)

bool invalid = IBANValidator.IsValid("IT00X0542811101000000123456"); // false (bad checksum)
```

---

## Validate a Partita IVA

```csharp
using CodiceFiscale;

// Standard company VAT
bool valid = ItalianVatCodeValidator.IsValid("00484960588", isConsumer: false, isFiscal: false); // true

// Consumer (natural person) VAT — first digit must be 8 or 9
bool consumer = ItalianVatCodeValidator.IsValid("85423511618", isConsumer: true, isFiscal: false); // true

// Accept a Codice Fiscale as a valid fiscal identifier
bool fiscal = ItalianVatCodeValidator.IsValid("RSSMRA85T10A562S", isConsumer: false, isFiscal: true); // true
```

