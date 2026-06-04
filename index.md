---
_layout: landing
landingData:
  heroSection:
    name: ItalianFiscalKit
    tagline: Italian fiscal compliance for .NET
    image: images/icon.svg
    features:
      - name: Fiscal Code
        icon: fa-id-card
        description: Validate, parse and generate Italian Fiscal Codes with full omocodia and foreign-born support.
        url: docs/validation.md
      - name: IBAN & VAT
        icon: fa-building-columns
        description: Validate Italian IBANs and Partita IVA numbers locally, with no external calls.
        url: docs/iban.md
      - name: Municipality DB
        icon: fa-map-location-dot
        description: Built-in database of 8 000+ Italian municipalities and 261 foreign countries with Belfiore codes.
        url: docs/getting-started.md
      - name: Zero Dependencies
        icon: fa-feather
        description: No external packages at runtime. .NET 9, NativeAOT-friendly, runs fully offline.
        url: docs/introduction.md
      - name: Coverage
        icon: fa-circle-check
        description: 167 tests covering validation, parsing, generation, IBAN, VAT and municipalities.
        url: coverage-report.md
      - name: Open Source
        icon: fa-code-branch
        description: MIT-licensed. Contributions welcome — check the contributing guide to get started.
        url: docs/contributing.md
---

# ItalianFiscalKit

A lightweight .NET library for Italian fiscal compliance: validate, parse and generate **Fiscal Code**, validate **Partita IVA** and **IBAN**, and query a built-in database of Italian municipalities and foreign countries (Belfiore codes). Everything runs locally, no HTTP calls, no data leaves your app.

[![NuGet](https://img.shields.io/nuget/v/ItalianFiscalKit.svg)](https://www.nuget.org/packages/ItalianFiscalKit)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ItalianFiscalKit.svg)](https://www.nuget.org/packages/ItalianFiscalKit)
[![CI](https://github.com/DennisTurco/ItalianFiscalKit/actions/workflows/ci.yml/badge.svg)](https://github.com/DennisTurco/ItalianFiscalKit/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub Stars](https://img.shields.io/github/stars/DennisTurco/ItalianFiscalKit?style=social)](https://github.com/DennisTurco/ItalianFiscalKit/stargazers)

## 1. Quick Start

Install via NuGet:

```bash
dotnet add package ItalianFiscalKit
```

```csharp
using ItalianFiscalKit;

// Validate
bool valid = FiscalCodeValidator.IsValid("RSSMRA85T10A562S"); // true

// Parse
FiscalCodeData data = FiscalCodeParser.Parse("RSSMRA85T10A562S");
Console.WriteLine(data.Gender);       // Male
Console.WriteLine(data.DateOfBirth);  // 10/12/1985
Console.WriteLine(data.BelfioreCode); // A562
```

## 2. Features

- ✅ Fiscal Code **validation** (format, date, Belfiore code, check digit)
- ✅ Fiscal Code **parsing** (extract gender, date of birth, municipality)
- ✅ **Foreign-born** support (Z-codes for 261 countries including historical ones)
- ✅ IBAN **validation**
- ✅ Partita IVA **validation**
- ✅ Zero external dependencies at runtime
- ✅ .NET 9 / NativeAOT friendly

## 3. [📖 Read the documentation →](docs/introduction.md)
