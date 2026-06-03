# Parsing

Once you have a valid Fiscal Code, you can decode it into its three pieces of personal data: gender, date of birth, and birthplace code. `FiscalCodeParser` gives you two ways to do that depending on how you want to handle invalid input.

| Method | What happens with invalid input | Returns |
|---|---|---|
| `Parse(string cf)` | Throws `InvalidFiscalCodeException` | `FiscalCodeData` |
| `TryParse(string cf, out FiscalCodeData? data)` | Returns `false`, sets `data` to `null` | `bool` |

## 1. `Parse`

```csharp
using ItalianFiscalKit;
using ItalianFiscalKit.Entities;
using ItalianFiscalKit.Enums;

FiscalCodeData data = FiscalCodeParser.Parse("RSSMRA85T10A562S");
```

Use `Parse` when you''re confident the input is already valid — for example, after running `IsValid` yourself — and you prefer a direct return value over an out parameter.

## 2. `TryParse`

Use `TryParse` whenever the input comes from a user, a form, or any external source. No exceptions to catch, no try/catch blocks needed.

```csharp
if (FiscalCodeParser.TryParse("RSSMRA85T10A562S", out FiscalCodeData? data))
{
    Console.WriteLine($"Gender:      {data.Gender}");       // Male
    Console.WriteLine($"DateOfBirth: {data.DateOfBirth}");  // 1985-12-10
    Console.WriteLine($"Belfiore:    {data.BelfioreCode}"); // A562
}
else
{
    Console.WriteLine("Invalid CF");
}
```

## 3. What you get back: `FiscalCodeData`

```csharp
public record FiscalCodeData(
    Gender   Gender,
    DateOnly DateOfBirth,
    string   BelfioreCode);
```

| Property | Type | Example |
|---|---|---|
| `Gender` | `Gender` | `Gender.Male` |
| `DateOfBirth` | `DateOnly` | `new DateOnly(1985, 12, 10)` |
| `BelfioreCode` | `string` | `"A562"` |

**Gender** is inferred from the day field: days 1–31 are male, 41–71 are female (the actual day is `encoded − 40`).

**Date of birth**: the year is reconstructed from its last two digits. If those two digits are ≤ the last two digits of the current year, the year is assumed to be in the 2000s; otherwise it''s 1900s. For example, `01` → 2001, `85` → 1985.

**Belfiore code**: the raw 4-character code as it appears in the CF. Italian codes start with a consonant; foreign country codes start with `Z`. You can resolve it to a full municipality name using `MunicipalityExtensions`.

## 4. The exception: `InvalidFiscalCodeException`

`Parse` runs `FiscalCodeValidator.IsValid` internally before decoding. If validation fails, you get:

```csharp
throw new InvalidFiscalCodeException($"The provided Fiscal Code ''{cf}'' is not valid");
```

Three patterns to deal with this:

```csharp
// Option 1 — TryParse (recommended for user input)
if (FiscalCodeParser.TryParse(input, out var data))
{
    // use data
}

// Option 2 — validate first, then parse
if (FiscalCodeValidator.IsValid(input))
{
    FiscalCodeData data = FiscalCodeParser.Parse(input);
}

// Option 3 — try/catch (when you need the exception message)
try
{
    FiscalCodeData data = FiscalCodeParser.Parse(input);
}
catch (InvalidFiscalCodeException ex)
{
    // handle invalid input
}
```

## 5. Examples

```csharp
// Male, born in Italy
FiscalCodeData mario = FiscalCodeParser.Parse("RSSMRA85T10A562S");
// Gender:       Male
// DateOfBirth:  1985-12-10
// BelfioreCode: A562  (Rome)

// Female, born abroad (Venezuela)
FiscalCodeData gabriella = FiscalCodeParser.Parse("MRNGRL01P55Z614K");
// Gender:       Female  (55 - 40 = day 15)
// DateOfBirth:  2001-09-15
// BelfioreCode: Z614  (Venezuela)

// TryParse with invalid input
bool ok = FiscalCodeParser.TryParse("INVALID", out FiscalCodeData? result);
// ok:     false
// result: null
```
