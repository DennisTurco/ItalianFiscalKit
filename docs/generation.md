# Generation

`FiscalCodeGenerator.Generate` builds a fully valid 16-character Fiscal Code from personal data. The check character is always computed automatically, so what you get back will always pass `FiscalCodeValidator.IsValid`.

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

## 1. Parameters

```csharp
public static string Generate(
    string   name,
    string   surname,
    DateOnly dateOfBirth,
    Gender   gender,
    string   belfioreCode
)
```

| Parameter | Type | Notes |
|---|---|---|
| `name` | `string` | First name — must be at least 3 characters |
| `surname` | `string` | Surname — must be at least 3 characters |
| `dateOfBirth` | `DateOnly` | Date of birth |
| `gender` | `Gender` | `Gender.Male` or `Gender.Female` |
| `belfioreCode` | `string` | 4-character Belfiore code of the birthplace — must exist in the embedded dataset |

Don''t know the Belfiore code? Use `MunicipalityExtensions` to look it up by name or CAP.

## 2. How the CF is assembled

### 2.1 Surname (positions 1-3)

Consonants first, then vowels. Padded with `X` if the name is too short.

| Surname | Code |
|---|---|---|
| Rossi | **RSS** |
| Fo | **FOX** |
| Li | **LIX** |

### 2.2 Name (positions 4-6)

- **4 or more consonants** → take the 1st, 3rd, and 4th consonant.
- **Fewer than 4** → same rule as the surname.

| Name | Consonants | Code |
|---|---|---|
| Luigi | L, G | **LGU** |
| Giovanni | G, V, N, N | **GNN** (1st/3rd/4th) |
| Maria | M, R | **MRA** |

### 2.3 Year (positions 7-8)

Last two digits of the birth year. `1985` → `85`.

### 2.4 Month (position 9)

| Jan | Feb | Mar | Apr | May | Jun | Jul | Aug | Sep | Oct | Nov | Dec |
|---|---|---|---|---|---|---|---|---|---|---|---|
| A | B | C | D | E | H | L | M | P | R | S | T |

### 2.5 Day (positions 10-11)

- Male → actual day, zero-padded (`05`, `30`)
- Female → day + 40 (`15` → `55`, `01` → `41`)

### 2.6 Belfiore code (positions 12-15)

The 4-character code, uppercased. Italian municipality codes start with a consonant; foreign country codes (for people born abroad) start with `Z`.

### 2.7 Check character (position 16)

Computed automatically from the first 15 characters using the official weighted-sum algorithm.

## 3. When it throws

`InvalidFiscalCodeDataException` is raised when:

| Condition | Message |
|---|---|
| `name` is null, empty, or shorter than 3 characters | `The provided Name '...' is not valid` |
| `surname` is null, empty, or shorter than 3 characters | `The provided Surname '...' is not valid` |
| `belfioreCode` is not 4 characters or not in the dataset | `The provided Belfiore code '...' is not valid` |

## 4. Examples

```csharp
using ItalianFiscalKit;
using ItalianFiscalKit.Enums;

// Male — Italian municipality
string cf1 = FiscalCodeGenerator.Generate("Luigi", "Verdi", new DateOnly(1970, 5, 30), Gender.Male, "F839");
// "VRDLGU70E30F839C"

// Female — Italian municipality (Roma = H501)
string cf2 = FiscalCodeGenerator.Generate("Maria", "Rossi", new DateOnly(1985, 3, 15), Gender.Female, "H501");
// "RSSMRA85C55H501V"

// Round-trip: generate then parse back
bool ok = FiscalCodeParser.TryParse(cf1, out FiscalCodeData? data);
// ok:                true
// data.Gender:       Gender.Male
// data.DateOfBirth:  new DateOnly(1970, 5, 30)
// data.BelfioreCode: "F839"
```

> [!NOTE]
> The Belfiore code must exist in the embedded dataset.
> Passing an invented or retired code will throw `InvalidFiscalCodeDataException`.
