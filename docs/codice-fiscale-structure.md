# Codice Fiscale Structure

The Italian *Codice Fiscale* is a 16-character alphanumeric string that uniquely identifies a natural person for tax purposes.

## Layout

```text
R  S  S  M  R  A  8  5   T    1  0  A  5  6  2  S
|  |  |  |  |  |  |  |   |    |  |  |  |  |  |  |
└──┴──┘  └──┴──┘  └──┘   ┴    └──┘  └──┴──┴──┘  ┴
surname    name   year  month  day   belfiore  check
 (3)       (3)    (2)    (1)   (2)     (4)      (1)
```

| Position | Length | Content | Example |
|---|---|---|---|
| 1–3 | 3 chars | Surname consonants/vowels | `RSS` |
| 4–6 | 3 chars | Name consonants/vowels | `MRA` |
| 7–8 | 2 digits | Year of birth (last 2 digits) | `85` → 1985 |
| 9 | 1 char | Month of birth (letter code) | `T` → December |
| 10–11 | 2 digits | Day of birth (female: day + 40) | `10` → day 10 |
| 12–15 | 4 chars | Belfiore catastal code | `A562` |
| 16 | 1 char | Check character | `S` |

---

## Surname encoding (positions 1–3)

Consonants are taken first (in order), then vowels. If fewer than 3 characters are available, `X` is used as padding.

| Surname | Consonants | Vowels | Code |
|---|---|---|---|
| Rossi | R, S, S | O, I | `RSS` |
| Re | R | E | `REX` |
| Fo | F | O | `FOX` |

---

## Name encoding (positions 4–6)

If the name has **4 or more consonants**, the 1st, 3rd, and 4th are taken.
Otherwise, the same consonants-then-vowels-then-X rule applies as for the surname.

| Name | Consonants | Code |
|---|---|---|
| Mario | M, R | `MRA` (M + R + vowel A) |
| Luca | L, C | `LCU` (L + C + vowel U) |
| Alessandro | L, S, S, N, D, R | `LSN` (1st, 3rd, 4th consonant) |

---

## Month encoding (position 9)

| Letter | Month |
|---|---|
| A | January |
| B | February |
| C | March |
| D | April |
| E | May |
| H | June |
| L | July |
| M | August |
| P | September |
| R | October |
| S | November |
| T | December |

---

## Day encoding (positions 10–11)

- **Male**: actual day of birth (`01`–`31`)
- **Female**: day of birth + 40 (`41`–`71`)

This allows unambiguous gender determination from the two-digit day field.

---

## Belfiore code (positions 12–15)

A 4-character code identifying the place of birth:

- **Italian municipality**: one letter (A–Z, excluding certain letters) + 3 digits, e.g. `A562` (Rome)
- **Foreign country**: always starts with `Z` + 3 digits, e.g. `Z614` (Venezuela)

The full dataset (~7 800 Italian municipalities + ~260 foreign countries) is embedded in the library.

---

## Check character (position 16)

Computed from the first 15 characters using a weighted sum algorithm:

1. Characters at **odd positions** (1, 3, 5, …) use a non-linear lookup table
2. Characters at **even positions** (2, 4, 6, …) use their ordinal value (0–25 for letters, 0–9 for digits)
3. Sum all values, take modulo 26, convert to letter (`A`=0, `B`=1, …)

This library validates the check character automatically inside `CodiceFiscaleValidator.IsValid`.

---

## Omocodia

When two people would share the same Codice Fiscale (same name, birthdate, and birthplace), one or more of the numeric digits (positions 7–8, 10–11) are replaced with letters according to a fixed substitution table:

| Digit | Letter |
|---|---|
| 0 | L |
| 1 | M |
| 2 | N |
| 3 | P |
| 4 | Q |
| 5 | R |
| 6 | S |
| 7 | T |
| 8 | U |
| 9 | V |

> [!NOTE]
> Omocodia support is not yet implemented in this library. Standard Codici Fiscali (all digits in numeric positions) are fully supported.
