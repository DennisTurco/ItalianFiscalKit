# Matching

## Overview

`CodiceFiscaleMatcher.Matches` verifies whether a given Codice Fiscale corresponds to a specific
person''s data. It generates the expected CF from the provided inputs and compares it to the
supplied string — returning `true` only if they are identical.

```csharp
using CodiceFiscale;
using CodiceFiscale.Enums;

bool match = CodiceFiscaleMatcher.Matches(
    cf:           "VRDLGU70E30F839C",
    name:         "Luigi",
    surname:      "Verdi",
    dateOfBirth:  new DateOnly(1970, 5, 30),
    gender:       Gender.Male,
    belfioreCode: "F839"
);
// true
```

The method is **case-insensitive** and trims surrounding whitespace from the CF before
comparing. It **never throws** — invalid or null inputs return `false`.

---

## Signature

```csharp
public static bool Matches(
    string   cf,
    string   name,
    string   surname,
    DateOnly dateOfBirth,
    Gender   gender,
    string   belfioreCode
)
```

| Parameter | Type | Description |
|---|---|---|
| `cf` | `string` | The Codice Fiscale to verify |
| `name` | `string` | Person''s first name (≥ 3 characters) |
| `surname` | `string` | Person''s surname (≥ 3 characters) |
| `dateOfBirth` | `DateOnly` | Date of birth |
| `gender` | `Gender` | `Gender.Male` or `Gender.Female` |
| `belfioreCode` | `string` | 4-character Belfiore code of place of birth |

---

## How it works

1. **Validate inputs** — if `cf` is null or empty, or if `name`/`surname`/`belfioreCode` would
   cause `CodiceFiscaleGenerator` to throw, `Matches` returns `false` immediately.
2. **Generate** the expected CF with `CodiceFiscaleGenerator.Generate(...)`.
3. **Normalize** the supplied `cf` with `CodiceFiscaleNormalizer.NormalizeOmocodia` to handle
   omocodia variants.
4. **Compare** the two strings (case-insensitive).

---

## Return value

| Condition | Returns |
|---|---|
| CF matches the generated code | `true` |
| `cf` is null, empty, or whitespace | `false` |
| `name` or `surname` shorter than 3 characters | `false` |
| `belfioreCode` not found in the dataset | `false` |
| Any personal data field does not match the CF | `false` |

---

## Examples

```csharp
using CodiceFiscale;
using CodiceFiscale.Enums;

// ✅ Correct data
CodiceFiscaleMatcher.Matches("VRDLGU70E30F839C", "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Male, "F839");
// true

// ✅ Case-insensitive
CodiceFiscaleMatcher.Matches("vrdlgu70e30f839c", "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Male, "F839");
// true

// ❌ Wrong date of birth
CodiceFiscaleMatcher.Matches("VRDLGU70E30F839C", "Luigi", "Verdi",
    new DateOnly(1971, 5, 30), Gender.Male, "F839");
// false

// ❌ Wrong gender
CodiceFiscaleMatcher.Matches("VRDLGU70E30F839C", "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Female, "F839");
// false

// ❌ Null CF
CodiceFiscaleMatcher.Matches(null!, "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Male, "F839");
// false
```

> [!NOTE]
> `Matches` generates the CF internally using `CodiceFiscaleGenerator`. The same encoding
> rules apply (surname consonants, name 1st/3rd/4th consonant rule, month letter, etc.).
> If the person''s name produces a different encoding than the one encoded in the CF
> (e.g. due to a hyphenated name or accent), `Matches` will return `false`.
