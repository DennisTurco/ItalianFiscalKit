# Introduction

**CodiceFiscale** is a .NET 9 library for working with Italian personal identifiers.

## What it does

| Feature | Class | Status |
|---|---|---|
| Validate a Codice Fiscale | `CodiceFiscaleValidator` | ✅ Available |
| Parse a Codice Fiscale into personal data | `CodiceFiscaleParser` | ✅ Available |
| Non-throwing parse (TryParse pattern) | `CodiceFiscaleParser.TryParse` | ✅ Available |
| Generate a Codice Fiscale from personal data | `CodiceFiscaleGenerator` | ✅ Available |
| Match a Codice Fiscale against personal data | `CodiceFiscaleMatcher` | ✅ Available |
| Validate an IBAN (SEPA countries) | `IBANValidator` | ✅ Available |
| Validate an Italian VAT code (Partita IVA) | `ItalianVatCodeValidator` | ✅ Available |

## Design principles

- **Local-only** — all logic runs in-process. No HTTP calls, no external services. Personal data never leaves your application.
- **Embedded datasets** — the municipality/Belfiore dataset (~7 800 comuni) and the foreign country codes (261 countries including historical ones) are compiled directly into the DLL as embedded resources. No files to deploy, no database to configure.
- **Lazy loading** — datasets are deserialized once on first use and cached for the lifetime of the process. Repeated calls are fast.
- **Immutable results** — all output types are C# `record`s.
- **Never throws on invalid input** — `IsValid` methods always return `bool`. Only `Parse` throws; use `TryParse` for exception-free parsing.

## Supported platforms

- .NET 9.0+

## License

MIT — see the [repository](https://github.com/DennisTurco/CodiceFiscale) for details.
