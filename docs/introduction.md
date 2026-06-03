# Introduction

**ItalianFiscalKit** is a .NET 9 library that makes working with Italian personal identifiers straightforward, no HTTP calls, no configuration files. Everything runs locally inside your app.

## 1. What you can do with it

| What | Class |
|---|---|
| Validate a Fiscal Code (CF) | `FiscalCodeValidator` |
| Decode a CF into gender, date of birth and birthplace | `FiscalCodeParser` |
| Parse without try/catch | `FiscalCodeParser.TryParse` |
| Generate a CF from personal data | `FiscalCodeGenerator` |
| Check whether a CF matches a person | `FiscalCodeMatcher` |
| Get age and adult-status from parsed data | `FiscalCodeExtensions` |
| Search municipalities by name, CAP, Belfiore code or province | `MunicipalityExtensions` |
| Validate an IBAN (all SEPA countries) | `IBANValidator` |
| Validate an Italian VAT number (Partita IVA) | `ItalianVatCodeValidator` |

## 2. How it's designed

- **Everything stays local**: validation runs entirely in-process. Fiscal codes and VAT numbers are personal data; they never leave your application.
- **No files to ship**: the municipality/Belfiore dataset (~7 800 municipalities) and 261 foreign country codes are baked directly into the DLL as embedded resources.
- **Fast after the first call**: datasets are loaded once on first use and kept in memory. Subsequent calls are just in-memory lookups.
- **Results are immutable**: all output types are C# `record`s, so you can safely pass them around without defensive copies.
- **`IsValid` never throws**: validation always returns a `bool`. Only `Parse` throws; if you'd rather avoid exceptions, `TryParse` has you covered.

## 3. Requirements

.NET 9.0 or later.

## 4. License

MIT, see the [repository](https://github.com/DennisTurco/ItalianFiscalKit) for details.
