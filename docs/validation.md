# Validation

## Overview

`CodiceFiscaleValidator.IsValid(string cf)` performs a multi-step validation of an Italian Codice Fiscale. It returns `true` only if **all** checks pass.

```csharp
using CodiceFiscale;

bool valid = CodiceFiscaleValidator.IsValid("RSSMRA85T10A562S"); // true
```

The method is **static** and **thread-safe**. It never throws — invalid or `null`-like inputs always return `false`.

---

## Validation pipeline

The validator applies the following checks in order, short-circuiting on the first failure:

### Step 1 — Basic format

- Input must not be null or empty
- Length must be exactly **16 characters**
- Must contain **no whitespace** (tabs, spaces, newlines all fail)

```csharp
CodiceFiscaleValidator.IsValid("RSSMRA85T10A562S ");  // false (trailing space)
CodiceFiscaleValidator.IsValid("RSSMRA85T10A562");    // false (15 chars)
```

### Step 2 — Regex structure

The input is matched against the pattern:

```
[A-Z]{3}         surname
[A-Z]{3}         name
[0-9]{2}         year
[ABCDEHILMPRST]  month letter
[0-9]{2}         day
\S{4}            belfiore code
[A-Z]            check character
```

Input is uppercased before matching, so lowercase letters are accepted.

```csharp
CodiceFiscaleValidator.IsValid("rssmra85t10a562s");  // true
CodiceFiscaleValidator.IsValid("RSS1RA85T10A562S");  // false (digit in name)
CodiceFiscaleValidator.IsValid("RSSMRA85110A562S");  // false (digit in month)
```

### Step 3 — Date coherence

The day and month are validated against the actual calendar:

- Months with 30 days (April `D`, June `H`, September `P`, November `S`): day 1–30 (male) or 41–70 (female)
- February `B`: day 1–28 or 1–29 for leap years (male) or 41–68/41–69 (female)
- Other months: day 1–31 (male) or 41–71 (female)

```csharp
CodiceFiscaleValidator.IsValid("RSSMRA85D31A562?");  // false (April has 30 days)
CodiceFiscaleValidator.IsValid("RSSMRA85B29A562?");  // false (1985 is not a leap year)
```

### Step 4 — Belfiore code

The 4-character Belfiore code is looked up in the embedded datasets:

- Codes starting with a **letter other than Z**: searched in the Italian municipalities dataset (~7 800 comuni)
- Codes starting with **Z**: searched in the foreign countries dataset (~260 countries)

```csharp
CodiceFiscaleValidator.IsValid("RSSMRA85T10Z999S");  // false (Z999 not assigned)
CodiceFiscaleValidator.IsValid("MRNGRL01P55Z614X");  // true  (Z614 = Venezuela)
```

### Step 5 — Check character

The 16th character is recomputed from the first 15 using the standard algorithm and compared to the actual value. See [Codice Fiscale Structure](codice-fiscale-structure.md#check-character-position-16) for details.

```csharp
CodiceFiscaleValidator.IsValid("RSSMRA85T10A562S");  // true  (S is correct)
CodiceFiscaleValidator.IsValid("RSSMRA85T10A562X");  // false (X is wrong)
```

---

## Return value summary

| Condition | Returns |
|---|---|
| All checks pass | `true` |
| Null, empty, or whitespace-only | `false` |
| Wrong length | `false` |
| Contains whitespace | `false` |
| Fails regex pattern | `false` |
| Impossible calendar date | `false` |
| Unknown Belfiore/Z code | `false` |
| Wrong check character | `false` |
