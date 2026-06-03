# VAT Validation (Partita IVA)

`ItalianVatCodeValidator.IsValid` validates Italian VAT codes (*Partita IVA*) according to the rules published by the Agenzia delle Entrate. It handles standard company VATs, natural-person VATs, and optionally accepts a Codice Fiscale as a valid fiscal identifier.

```csharp
using ItalianFiscalKit;

bool valid = ItalianVatCodeValidator.IsValid("00484960588", isConsumer: false, isFiscal: false);
// true
```

The method is static, thread-safe, and never throws.

---

## Parameters

| Parameter | What it does |
|---|---|
| `vat` | The string to validate — an 11-digit Partita IVA, or a 16-character CF if `isFiscal` is `true` |
| `isConsumer` | Set to `true` for natural persons: enforces that the first digit is `8` or `9` |
| `isFiscal` | Set to `true` to also accept a valid 16-character Codice Fiscale |

---

## The checksum algorithm

A standard 11-digit Partita IVA is validated in three steps:

1. **Length** — must be exactly 11 digits, all numeric
2. **Consumer flag** — if `isConsumer: true`, the first digit must be `8` or `9`; otherwise it must be `0`–`7`
3. **Luhn-like checksum** (Agenzia delle Entrate algorithm):
   - Sum digits at **odd positions** (1, 3, 5, 7, 9) directly
   - Double digits at **even positions** (2, 4, 6, 8, 10); subtract 9 if the result is ≥ 10
   - The 11th digit must equal `(10 − (sum % 10)) % 10`

---

## Combining the flags

| `isConsumer` | `isFiscal` | What''s accepted |
|---|---|---|
| `false` | `false` | 11-digit VAT, first digit 0–7 |
| `true` | `false` | 11-digit VAT, first digit 8–9 |
| `false` | `true` | 11-digit VAT (any) **or** valid 16-char Codice Fiscale |
| `true` | `true` | Consumer VAT (first digit 8–9) **or** valid Codice Fiscale |

---

## Examples

```csharp
// Standard company VAT
ItalianVatCodeValidator.IsValid("00484960588", false, false); // true
ItalianVatCodeValidator.IsValid("10433218194", false, false); // true

// Natural person (first digit 8 or 9)
ItalianVatCodeValidator.IsValid("85423511618", true, false);  // true
ItalianVatCodeValidator.IsValid("91849593107", true, false);  // true

// Codice Fiscale accepted as fiscal identifier
ItalianVatCodeValidator.IsValid("RSSMRA85T10A562S", false, true); // true
ItalianVatCodeValidator.IsValid("00484960588",      false, true); // true — VAT also accepted

// Invalid cases
ItalianVatCodeValidator.IsValid("",            false, false); // false — empty
ItalianVatCodeValidator.IsValid("  ",          false, false); // false — whitespace
ItalianVatCodeValidator.IsValid("0218208039",  false, false); // false — 10 digits, too short
ItalianVatCodeValidator.IsValid("021820803900",false, false); // false — 12 digits, too long
ItalianVatCodeValidator.IsValid("02182080391", false, false); // false — bad checksum
ItalianVatCodeValidator.IsValid("80025291002", false, false); // false — first digit 8, but isConsumer=false
ItalianVatCodeValidator.IsValid("02182080390", true,  false); // false — first digit 0, but isConsumer=true
ItalianVatCodeValidator.IsValid("RSSMRA85T10A562S", false, false); // false — CF not accepted without isFiscal=true
```
