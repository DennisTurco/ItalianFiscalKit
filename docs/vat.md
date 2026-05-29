# VAT Validation (Partita IVA)

## Overview

`ItalianVatCodeValidator.IsValid(string vat, bool isConsumer, bool isFiscal)` validates Italian VAT codes (*Partita IVA*) according to the rules defined by the Agenzia delle Entrate.

```csharp
using CodiceFiscale;

bool valid = ItalianVatCodeValidator.IsValid("00484960588", isConsumer: false, isFiscal: false);
// true
```

The method is **static** and **thread-safe**. It never throws.

---

## Parameters

| Parameter | Type | Description |
|---|---|---|
| `vat` | `string` | The code to validate. Either an 11-digit Partita IVA or a 16-character Codice Fiscale (if `isFiscal` is `true`). |
| `isConsumer` | `bool` | `true` if the code belongs to a natural person (consumer). The first digit must be `8` or `9`. |
| `isFiscal` | `bool` | `true` to also accept a 16-character Codice Fiscale as a valid identifier. |

---

## Validation algorithm

A standard 11-digit Partita IVA is validated as follows:

1. **Length** must be exactly 11 digits (all numeric)
2. **Consumer flag** — if `isConsumer: true`, first digit must be `8` or `9`; if `false`, first digit must be `0`–`7`
3. **Luhn-like checksum** (Agenzia delle Entrate algorithm):
   - Sum digits at **odd positions** (1, 3, 5, 7, 9) directly
   - Double digits at **even positions** (2, 4, 6, 8, 10); if the result ≥ 10, subtract 9
   - The 11th digit must equal `(10 - (sum % 10)) % 10`

---

## Mode combinations

| `isConsumer` | `isFiscal` | Accepts |
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

// Consumer VAT (first digit 8 or 9)
ItalianVatCodeValidator.IsValid("85423511618", true, false);  // true
ItalianVatCodeValidator.IsValid("91849593107", true, false);  // true

// Codice Fiscale accepted as fiscal identifier
ItalianVatCodeValidator.IsValid("RSSMRA85T10A562S", false, true); // true
ItalianVatCodeValidator.IsValid("00484960588",      false, true); // true (VAT also accepted)

// Invalid cases
ItalianVatCodeValidator.IsValid("",            false, false); // false (empty)
ItalianVatCodeValidator.IsValid("  ",          false, false); // false (whitespace)
ItalianVatCodeValidator.IsValid("0218208039",  false, false); // false (10 digits, too short)
ItalianVatCodeValidator.IsValid("021820803900",false, false); // false (12 digits, too long)
ItalianVatCodeValidator.IsValid("02182080391", false, false); // false (bad checksum)
ItalianVatCodeValidator.IsValid("80025291002", false, false); // false (first digit 8, but isConsumer=false)
ItalianVatCodeValidator.IsValid("02182080390", true,  false); // false (first digit 0, but isConsumer=true)
ItalianVatCodeValidator.IsValid("RSSMRA85T10A562S", false, false); // false (CF not accepted without isFiscal)
```
