# Parsing

## Overview

`CodiceFiscaleParser.Parse(string cf)` decodes a valid Codice Fiscale into its constituent personal data.

```csharp
using CodiceFiscale;
using CodiceFiscale.Entities;
using CodiceFiscale.Enums;

CodiceFiscaleData data = CodiceFiscaleParser.Parse("RSSMRA85T10A562S");
```

---

## Return type: `CodiceFiscaleData`

```csharp
public record CodiceFiscaleData(
    Gender   Gender,
    DateOnly DateOfBirth,
    string   BelfioreCode);
```

| Property | Type | Description | Example |
|---|---|---|---|
| `Gender` | `Gender` | `Male` or `Female` | `Gender.Male` |
| `DateOfBirth` | `DateOnly` | Full date of birth | `new DateOnly(1985, 12, 10)` |
| `BelfioreCode` | `string` | 4-char Belfiore code of place of birth | `"A562"` |

### Gender

Gender is inferred from the day field:
- Day **1–31** → `Gender.Male`
- Day **41–71** → `Gender.Female` (actual day = encoded day − 40)

### Date of birth

The year is reconstructed from the 2-digit year using the current year as cutoff:
- If the 2-digit year ≤ current year's last 2 digits → 2000s (e.g. `01` → 2001)
- Otherwise → 1900s (e.g. `85` → 1985)

### Belfiore code

The raw 4-character code is returned as-is. Italian municipality codes start with a letter (other than Z); foreign country codes start with `Z`.

---

## Exception: `InvalidCodiceFiscaleException`

`Parse` runs `CodiceFiscaleValidator.IsValid` internally. If the input is not valid, it throws:

```csharp
throw new InvalidCodiceFiscaleException($"The provided Codice Fiscale '{cf}' is not valid");
```

Always validate before parsing, or handle the exception:

```csharp
// Option 1 — validate first
if (CodiceFiscaleValidator.IsValid(input))
{
    CodiceFiscaleData data = CodiceFiscaleParser.Parse(input);
}

// Option 2 — try/catch
try
{
    CodiceFiscaleData data = CodiceFiscaleParser.Parse(input);
}
catch (InvalidCodiceFiscaleException ex)
{
    // handle invalid input
}
```

---

## Examples

```csharp
// Male, born in Italy
CodiceFiscaleData mario = CodiceFiscaleParser.Parse("RSSMRA85T10A562S");
// Gender:       Male
// DateOfBirth:  1985-12-10
// BelfioreCode: A562  (Rome)

// Female, born abroad (Venezuela)
CodiceFiscaleData gabriella = CodiceFiscaleParser.Parse("MRNGRL01P55Z614X");
// Gender:       Female  (55 - 40 = day 15)
// DateOfBirth:  2001-09-15
// BelfioreCode: Z614  (Venezuela)
```
