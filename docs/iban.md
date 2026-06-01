# IBAN Validation

## Overview

`IBANValidator.IsValid(string iban)` validates an International Bank Account Number according to the ISO 13616 standard using the mod-97 algorithm.

> [!NOTE]
> Only **SEPA** country codes are accepted (36 countries: EU27 + UK, Switzerland, Norway, Iceland, Liechtenstein, Monaco, San Marino, Andorra, Vatican City). Non-SEPA countries such as the USA, Canada, or Australia do not use IBAN and will return `false`.

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

1. **Strip whitespace** and uppercase the input
2. **Check country code** — the first 2 characters must be a SEPA member country code
3. **Check expected length** — each country has a fixed IBAN length (e.g. Italy: 27, Germany: 22, UK: 22, France: 27)
4. **Rearrange** — move the first 4 characters to the end
5. **Convert letters to digits** — A=10, B=11, …, Z=35
6. **Compute mod 97** — the result must equal `1`

---

## SEPA countries supported

| Country | Code | Length | Country | Code | Length |
|---|---|---|---|---|---|
| Austria | AT | 20 | Netherlands | NL | 18 |
| Belgium | BE | 16 | Norway | NO | 15 |
| Bulgaria | BG | 22 | Poland | PL | 28 |
| Croatia | HR | 21 | Portugal | PT | 25 |
| Cyprus | CY | 28 | Romania | RO | 24 |
| Czech Republic | CZ | 24 | San Marino | SM | 27 |
| Denmark | DK | 18 | Slovakia | SK | 24 |
| Estonia | EE | 20 | Slovenia | SI | 19 |
| Finland | FI | 18 | Spain | ES | 24 |
| France | FR | 27 | Sweden | SE | 24 |
| Germany | DE | 22 | Switzerland | CH | 21 |
| Greece | GR | 27 | United Kingdom | GB | 22 |
| Hungary | HU | 28 | Vatican City | VA | 22 |
| Iceland | IS | 26 | Andorra | AD | 24 |
| Ireland | IE | 22 | Liechtenstein | LI | 21 |
| Italy | IT | 27 | Monaco | MC | 27 |
| Latvia | LV | 21 | Luxembourg | LU | 20 |
| Lithuania | LT | 20 | Malta | MT | 31 |

---

## Examples

```csharp
// Valid IBANs
IBANValidator.IsValid("IT60X0542811101000000123456");   // Italy
IBANValidator.IsValid("DE89370400440532013000");        // Germany
IBANValidator.IsValid("GB29NWBK60161331926819");        // United Kingdom
IBANValidator.IsValid("FR7630006000011234567890189");   // France
IBANValidator.IsValid("ES9121000418450200051332");      // Spain
IBANValidator.IsValid("CH5604835012345678009");         // Switzerland
IBANValidator.IsValid("NO9386011117947");               // Norway

// Invalid IBANs
IBANValidator.IsValid("");                              // false (empty)
IBANValidator.IsValid("IT00X0542811101000000123456");   // false (bad mod-97 checksum)
IBANValidator.IsValid("XX60X0542811101000000123456");   // false (unknown country XX)
IBANValidator.IsValid("US60X0542811101000000123456");   // false (non-SEPA country)
IBANValidator.IsValid("IT60X054281110100000012345");    // false (wrong length: 26 instead of 27)
```
