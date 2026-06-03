# Validation

`FiscalCodeValidator.IsValid` is the simplest entry point in the library. Give it any string and it tells you, with a plain `bool`, whether it''s a valid Italian Codice Fiscale.

```csharp
using ItalianFiscalKit;

bool valid = FiscalCodeValidator.IsValid("RSSMRA85T10A562S"); // true
```

The method is static, thread-safe, and never throws — you can call it from anywhere without wrapping it in try/catch.

---

## What it checks

The validator runs five checks in sequence and stops at the first failure, so it''s also fast for invalid input.

### Step 1 — Basic sanity

- Not null or empty
- Exactly **16 characters**
- No whitespace (a trailing space is enough to fail)

```csharp
FiscalCodeValidator.IsValid("RSSMRA85T10A562S ");  // false — trailing space
FiscalCodeValidator.IsValid("RSSMRA85T10A562");    // false — 15 chars
```

### Step 2 — Structure (regex)

The input is uppercased and matched against the expected pattern: three letters for the surname, three for the name, two digits for the year, one month letter, two digits for the day, four characters for the Belfiore code, one final letter for the check character.

```csharp
FiscalCodeValidator.IsValid("rssmra85t10a562s");  // true  — lowercase accepted
FiscalCodeValidator.IsValid("RSS1RA85T10A562S");  // false — digit where a letter is expected
FiscalCodeValidator.IsValid("RSSMRA85110A562S");  // false — digit in month position
```

### Step 3 — Calendar coherence

The day and month are checked against a real calendar — including leap years for February.

- April `D`, June `H`, September `P`, November `S` → max 30 days (male) / 70 (female)
- February `B` → 28 or 29 depending on the year (male) / 68–69 (female)
- All other months → max 31 days (male) / 71 (female)

```csharp
FiscalCodeValidator.IsValid("RSSMRA85D31A562?");  // false — April only has 30 days
FiscalCodeValidator.IsValid("RSSMRA85B29A562?");  // false — 1985 is not a leap year
```

### Step 4 — Belfiore code lookup

The 4-character birthplace code is looked up in the embedded datasets:

- Starts with a letter other than Z → Italian municipality (~7 800 comuni)
- Starts with Z → foreign country (~260 countries, including historical ones)

```csharp
FiscalCodeValidator.IsValid("RSSMRA85T10Z999S");  // false — Z999 doesn''t exist
FiscalCodeValidator.IsValid("MRNGRL01P55Z614X");  // true  — Z614 is Venezuela
```

### Step 5 — Check character

The 16th character is recomputed from the first 15 and compared to the actual value. A single wrong character anywhere in the CF will change the expected check digit and make validation fail.

```csharp
FiscalCodeValidator.IsValid("RSSMRA85T10A562S");  // true  — S is correct
FiscalCodeValidator.IsValid("RSSMRA85T10A562X");  // false — X is wrong
```

---

## Quick reference

| Input | Returns |
|---|---|
| All checks pass | `true` |
| Null, empty, or whitespace | `false` |
| Wrong length | `false` |
| Contains whitespace | `false` |
| Fails regex pattern | `false` |
| Impossible calendar date | `false` |
| Unknown Belfiore or Z code | `false` |
| Wrong check character | `false` |
