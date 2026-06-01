# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-06-01

### Added

#### Codice Fiscale

- `CodiceFiscaleValidator.IsValid(string cf)` — full 5-step validation pipeline: regex format, birth date, Belfiore code, check digit
- `CodiceFiscaleParser.Parse(string cf)` — decodes a CF into `CodiceFiscaleData` (gender, date of birth, Belfiore code)
- `CodiceFiscaleParser.TryParse(string cf, out CodiceFiscaleData? data)` — non-throwing alternative to `Parse`
- `InvalidCodiceFiscaleException` — dedicated exception type for invalid CF inputs
- **Foreign-born support** — Belfiore codes starting with `Z` (e.g. `Z614` = Venezuela) are validated against a dataset of 261 foreign countries (active + historical/soppresso)
- `CodiceFiscaleData` — immutable `record` with `Gender`, `DateOfBirth`, `BelfioreCode`
- `Gender` enum with `Male` and `Female` values

#### IBAN

- `IBANValidator.IsValid(string iban)` — validates IBAN using the ISO 13616 mod-97 algorithm
- Supports all **36 SEPA countries** (EU27 + UK, Switzerland, Norway, Iceland, Liechtenstein, Monaco, San Marino, Andorra, Vatican City)
- Each country's expected IBAN length is enforced separately
- Spaces in the input are accepted and stripped before validation

#### Partita IVA

- `ItalianVatCodeValidator.IsValid(string vat, bool isConsumer, bool isFiscal)` — validates Italian VAT codes using the Luhn-like Agenzia delle Entrate algorithm
- `isConsumer` flag enforces first-digit rule (8–9 for natural persons, 0–7 for companies)
- `isFiscal` flag enables accepting a 16-character Codice Fiscale as a valid identifier

#### Datasets (embedded resources)

- `Resources/municipalities.json` — ~7 800 Italian comuni with Belfiore code, province, region, coordinates
- `Resources/foreign_countries.json` — 261 foreign country codes (active + historical soppresso states: Czechoslovakia, East Germany, USSR, Yugoslavia, …)
- `Resources/check_code_map.json` — odd/even value table for the Codice Fiscale check digit algorithm

#### Infrastructure

- `DataStore` — internal `Lazy<T>` singleton cache; all three datasets are deserialized once on first use and cached for the lifetime of the process (thread-safe, no locks required)
- `Config` — centralized embedded resource name constants
- `CodiceFiscaleTokenizer` — internal regex-based CF tokenizer used by Validator and Parser

#### Documentation

- Full [DocFX](https://dotnet.github.io/docfx/) documentation site published to GitHub Pages
- API reference generated from XML doc comments (zero CS1591 warnings)
- Conceptual docs: introduction, getting started, CF structure, validation, parsing, IBAN, VAT

### Notes

- All public methods are **static** and **thread-safe**
- No HTTP calls, no external services — all validation runs locally in-process
- Targets **.NET 9.0**

## [0.1.0] - 2026-05-22

### Added

- Initial project structure
- Basic Codice Fiscale validation (Italian municipalities only)
- Basic IBAN and VAT stubs (not yet implemented)

[1.0.0]: https://github.com/DennisTurco/CodiceFiscale/releases/tag/v1.0.0
[0.1.0]: https://github.com/DennisTurco/CodiceFiscale/releases/tag/v0.1.0
