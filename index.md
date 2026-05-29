---
_layout: landing
---

# CodiceFiscale

A lightweight .NET 9 library for Italian fiscal codes (*Codice Fiscale*), VAT numbers (*Partita IVA*), and IBAN validation.

All validation runs **locally** — no HTTP calls, no external services, no personal data leaves your application.

[![NuGet](https://img.shields.io/nuget/v/CodiceFiscale.svg)](https://www.nuget.org/packages/CodiceFiscale)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Quick Start

Install via NuGet:

```bash
dotnet add package CodiceFiscale
```

```csharp
using CodiceFiscale;

// Validate
bool valid = CodiceFiscaleValidator.IsValid("RSSMRA85T10A562S"); // true

// Parse
CodiceFiscaleData data = CodiceFiscaleParser.Parse("RSSMRA85T10A562S");
Console.WriteLine(data.Gender);       // Male
Console.WriteLine(data.DateOfBirth);  // 10/12/1985
Console.WriteLine(data.BelfioreCode); // A562
```

## Features

- ✅ Codice Fiscale **validation** (format, date, Belfiore code, check digit)
- ✅ Codice Fiscale **parsing** (extract gender, date of birth, municipality)
- ✅ **Foreign-born** support (Z-codes for 261 countries including historical ones)
- ✅ IBAN **validation**
- ✅ Partita IVA **validation**
- ✅ Zero external dependencies at runtime
- ✅ .NET 9 / NativeAOT friendly

## [📖 Read the documentation →](docs/introduction.md)
