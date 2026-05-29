# IBAN Validation

## Overview

`IBANValidator.IsValid(string iban)` validates an International Bank Account Number according to the ISO 13616 standard using the mod-97 algorithm.

```csharp
using CodiceFiscale;

bool valid = IBANValidator.IsValid("IT60X0542811101000000123456"); // true
```

The method is **static** and **thread-safe**. It never throws.

---

## Supported formats

Spaces are accepted and stripped before validation, so both compact and formatted IBANs are valid inputs:

```csharp
IBANValidator.IsValid("IT60X0542811101000000123456");        // true — compact
IBANValidator.IsValid("IT60 X054 2811 1010 0000 0123 456"); // true — formatted
```

---

## Validation algorithm

1. **Strip whitespace** from the input
2. **Check country code** — the first 2 characters must be a valid ISO 3166-1 alpha-2 country code that participates in the IBAN scheme
3. **Check expected length** — each country has a fixed IBAN length (e.g. Italy: 27, Germany: 22, UK: 22)
4. **Rearrange** — move the first 4 characters to the end
5. **Convert letters to digits** — A=10, B=11, …, Z=35
6. **Compute mod 97** — the result must equal `1`

---

## Examples

```csharp
// Valid IBANs
IBANValidator.IsValid("IT60X0542811101000000123456");  // Italy
IBANValidator.IsValid("DE89370400440532013000");       // Germany
IBANValidator.IsValid("GB29NWBK60161331926819");       // United Kingdom
IBANValidator.IsValid("FR7630006000011234567890189"); // France
IBANValidator.IsValid("ES9121000418450200051332");    // Spain

// Invalid IBANs
IBANValidator.IsValid("");                              // false (empty)
IBANValidator.IsValid("IT00X0542811101000000123456");  // false (bad mod-97 checksum)
IBANValidator.IsValid("XX60X0542811101000000123456");  // false (unknown country XX)
IBANValidator.IsValid("IT60X054281110100000012345");   // false (wrong length for Italy)
```
