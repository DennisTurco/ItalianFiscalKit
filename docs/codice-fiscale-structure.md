# Codice Fiscale Structure

The Italian *Codice Fiscale* is a 16-character alphanumeric string that uniquely identifies a person for tax purposes. Once you understand how it''s built, you can read gender, date of birth and birthplace directly from the string itself.

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
| 1–3 | 3 chars | Surname (consonants, then vowels) | `RSS` |
| 4–6 | 3 chars | Name (consonants, then vowels) | `MRA` |
| 7–8 | 2 digits | Year of birth — last 2 digits | `85` → 1985 |
| 9 | 1 char | Month of birth (letter code) | `T` → December |
| 10–11 | 2 digits | Day of birth (females: day + 40) | `10` → day 10 |
| 12–15 | 4 chars | Belfiore cadastral code of birthplace | `A562` |
| 16 | 1 char | Check character | `S` |

---

## Surname — positions 1–3

Consonants come first (in order), then vowels. If there aren''t enough characters to fill three positions, `X` is used as padding.

| Surname | Consonants | Vowels | Code |
|---|---|---|---|
| Rossi | R, S, S | O, I | `RSS` |
| Re | R | E | `REX` |
| Fo | F | O | `FOX` |

---

## Name — positions 4–6

Names with **4 or more consonants** follow a special rule: take the 1st, 3rd, and 4th consonant. For all other names, the same consonants → vowels → X padding rule applies.

| Name | Consonants | Code |
|---|---|---|
| Mario | M, R | `MRA` (M + R + vowel A) |
| Luca | L, C | `LCU` (L + C + vowel U) |
| Alessandro | L, S, S, N, D, R | `LSN` (1st, 3rd, 4th consonant) |

---

## Month — position 9

Each month is mapped to a fixed letter to keep the field purely alphabetic.

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

## Day — positions 10–11

- **Male**: the actual day of birth, zero-padded (`01`–`31`)
- **Female**: day of birth plus 40 (`41`–`71`)

This trick lets you determine gender unambiguously just from two digits.

---

## Belfiore code — positions 12–15

A 4-character code that identifies the birthplace:

- **Italian municipality**: one letter + 3 digits, e.g. `A562` (Rome) or `H501` (also Rome, different district)
- **Foreign country**: always starts with `Z` + 3 digits, e.g. `Z614` (Venezuela)

The full dataset — ~7 800 Italian municipalities plus ~260 foreign countries (including historical ones like the USSR and Yugoslavia) — is embedded in the library. No external lookup needed.

---

## Check character — position 16

The final character is a checksum computed from the first 15 characters:

1. Characters at **odd positions** (1, 3, 5, …) are mapped through a non-linear lookup table
2. Characters at **even positions** (2, 4, 6, …) use their ordinal value (0–25 for letters, 0–9 for digits)
3. Sum all values, take modulo 26, and convert to a letter (`A` = 0, `B` = 1, …)

`CodiceFiscaleValidator.IsValid` always verifies this character automatically — you don''t need to compute it yourself.

---

## Omocodia

When two people would share the exact same CF — same name, same birthdate, same birthplace — one or more of the numeric digits in positions 7–8 and 10–11 are replaced with a letter using a fixed substitution table:

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
> Omocodia variants are not yet supported by this library. Standard Codici Fiscali (with digits in all numeric positions) are fully handled.
