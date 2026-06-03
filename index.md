---
_layout: landing
---

# ItalianFiscalKit

A lightweight .NET library for Italian fiscal compliance: validate, parse and generate **Codice Fiscale**, validate **Partita IVA** and **IBAN**, and query a built-in database of Italian municipalities and foreign countries (Belfiore codes). Everything runs locally, no HTTP calls, no data leaves your app.

[![NuGet](https://img.shields.io/nuget/v/ItalianFiscalKit.svg)](https://www.nuget.org/packages/ItalianFiscalKit)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Quick Start

Install via NuGet:

```bash
dotnet add package ItalianFiscalKit
```

```csharp
using ItalianFiscalKit;

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
