# Generation

## Overview

`CodiceFiscaleGenerator.Generate` builds a valid 16-character Codice Fiscale from personal data.

```csharp
using CodiceFiscale;
using CodiceFiscale.Enums;

string cf = CodiceFiscaleGenerator.Generate(
    name:        "Luigi",
    surname:     "Verdi",
    dateOfBirth: new DateOnly(1970, 5, 30),
    gender:      Gender.Male,
    belfioreCode: "F839"
);
// "VRDLGU70E30F839C"
```

The method throws `InvalidCodiceFiscaleDataException` if any input is invalid. It never returns a structurally wrong CF — the check character is always recomputed from the first 15 characters.

---

## Signature

```csharp
public static string Generate(
    string   name,
    string   surname,
    DateOnly dateOfBirth,
    Gender   gender,
    string   belfioreCode
)
```

| Parameter | Type | Description |
|---|---|---|
| `name` | `string` | Person's first name (≥ 3 characters) |
| `surname` | `string` | Person's surname (≥ 3 characters) |
| `dateOfBirth` | `DateOnly` | Date of birth |
| `gender` | `Gender` | `Gender.Male` or `Gender.Female` |
| `belfioreCode` | `string` | 4-character Belfiore (cadastral) code of place of birth |

---

## Encoding algorithm

The generator applies the official Italian Codice Fiscale algorithm in 7 steps:

### 1 — Surname code (positions 1–3)

Take the consonants of the surname first, then the vowels. Pad with `X` if fewer than 3 characters.

| Surname | Consonants | Vowels | Code |
|---|---|---|---|
| Rossi | RSS | OI | **RSS** |
| Fo | F | O | **FOX** |
| Li | L | I | **LIX** |

### 2 — Name code (positions 4–6)

- If the name has **4 or more consonants**: take the **1st, 3rd, and 4th** consonant.
- Otherwise: same rule as surname (consonants → vowels → X padding).

| Name | Consonants | Rule | Code |
|---|---|---|---|
| Luigi | LG | < 4 consonants → consonants + vowels | **LGU** |
| Giovanni | GVN + N = GVNN | 4 consonants → 1st/3rd/4th | **GNN** |
| Maria | MR | < 4 → MR + A | **MRA** |

### 3 — Year (positions 7–8)

Last two digits of the birth year.

`1985` → `85`

### 4 — Month (position 9)

Each month is mapped to a fixed letter:

| Jan | Feb | Mar | Apr | May | Jun | Jul | Aug | Sep | Oct | Nov | Dec |
|---|---|---|---|---|---|---|---|---|---|---|---|
| A | B | C | D | E | H | L | M | P | R | S | T |

### 5 — Day (positions 10–11)

- **Male**: day as-is, zero-padded to 2 digits (`05`, `30`).
- **Female**: day + 40 (`15` → `55`, `01` → `41`).

### 6 — Belfiore code (positions 12–15)

The 4-character municipality or foreign country code, uppercased. Italian codes start with a consonant; foreign country codes (from the Z-list) start with `Z`.

To find your municipality's Belfiore code you can use the official [Agenzia delle Entrate](https://www.agenziaentrate.gov.it) lookup, or any Italian municipality database.

### 7 — Check character (position 16)

Computed from the first 15 characters using the official lookup table. Odd-position characters and even-position characters have separate weight tables; the sum modulo 26 maps to a letter A–Z.

---

## Exceptions

`InvalidCodiceFiscaleDataException` is thrown when:

| Condition | Message |
|---|---|
| `name` is null, empty, or shorter than 3 characters | `The provided Name '...' is not valid` |
| `surname` is null, empty, or shorter than 3 characters | `The provided Surname '...' is not valid` |
| `belfioreCode` is not 4 characters or not found in the embedded dataset | `The provided Belfiore code '...' is not valid` |

---

## Examples

```csharp
using CodiceFiscale;
using CodiceFiscale.Enums;

// Male — Italian municipality (Roma = F839)
string cf1 = CodiceFiscaleGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
// "VRDLGU70E30F839C"

// Female — Italian municipality (Napoli = F839... use real code)
string cf2 = CodiceFiscaleGenerator.Generate("Maria", "Rossi", new DateOnly(1985, 3, 15), Gender.Female, "H501");
// "RSSMRA85C55H501V"

// Round-trip: generate then parse
bool ok = CodiceFiscaleParser.TryParse(cf1, out CodiceFiscaleData? data);
// ok:             true
// data.Gender:    Gender.Male
// data.DateOfBirth: new DateOnly(1970, 5, 30)
// data.BelfioreCode: "F839"
```

> [!NOTE]
> The Belfiore code must exist in the embedded dataset (Italian municipalities or foreign countries).
> Passing an invented or retired code will throw `InvalidCodiceFiscaleDataException`.
