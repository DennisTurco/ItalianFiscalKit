# Matching

`FiscalCodeMatcher.Matches` answers a very practical question: *does this CF actually belong to this person?* It generates the expected CF from the provided data and compares it to the one you supply — returning `true` only when they match.

```csharp
using ItalianFiscalKit;
using CodiceFiscale.Enums;

bool match = FiscalCodeMatcher.Matches(
    cf:           "VRDLGU70E30F839C",
    name:         "Luigi",
    surname:      "Verdi",
    dateOfBirth:  new DateOnly(1970, 5, 30),
    gender:       Gender.Male,
    belfioreCode: "F839"
);
// true
```

The comparison is case-insensitive and ignores surrounding whitespace in the CF. The method never throws — any invalid or null input returns `false`.

---

## Parameters

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

| Parameter | Notes |
|---|---|
| `cf` | The Codice Fiscale to verify |
| `name` | First name (at least 3 characters) |
| `surname` | Surname (at least 3 characters) |
| `dateOfBirth` | Date of birth |
| `gender` | `Gender.Male` or `Gender.Female` |
| `belfioreCode` | 4-character Belfiore code of the birthplace |

---

## How it works

1. If `cf` is null or empty, returns `false` immediately.
2. Calls `FiscalCodeGenerator.Generate(...)` with the provided personal data to build the expected CF.
3. Compares the two strings, case-insensitively.
4. If `Generate` throws (e.g. invalid Belfiore code), the exception is caught internally and `false` is returned.

---

## Return value

| Situation | Returns |
|---|---|
| CF matches the generated code | `true` |
| `cf` is null, empty, or whitespace | `false` |
| `name` or `surname` is shorter than 3 characters | `false` |
| `belfioreCode` not found in the dataset | `false` |
| Any field doesn''t match | `false` |

---

## Examples

```csharp
using ItalianFiscalKit;
using CodiceFiscale.Enums;

// ✅ Correct data
FiscalCodeMatcher.Matches("VRDLGU70E30F839C", "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Male, "F839");
// true

// ✅ Case-insensitive
FiscalCodeMatcher.Matches("vrdlgu70e30f839c", "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Male, "F839");
// true

// ❌ Wrong date of birth
FiscalCodeMatcher.Matches("VRDLGU70E30F839C", "Luigi", "Verdi",
    new DateOnly(1971, 5, 30), Gender.Male, "F839");
// false

// ❌ Wrong gender
FiscalCodeMatcher.Matches("VRDLGU70E30F839C", "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Female, "F839");
// false

// ❌ Null CF
FiscalCodeMatcher.Matches(null!, "Luigi", "Verdi",
    new DateOnly(1970, 5, 30), Gender.Male, "F839");
// false
```

> [!NOTE]
> The matching is purely algorithmic — it re-encodes the name according to the official rules.
> If a person''s name has a hyphen, an accent, or any non-standard character that changes
> how it would be encoded, `Matches` may return `false` even if the CF is genuinely theirs.
