# Contributing

Thank you for your interest in contributing to CodiceFiscale.

## Before You Start

* Search existing issues before opening a new one.
* For bug reports, include enough information to reproduce the problem.
* For feature requests, explain the use case and expected behaviour.

## Reporting Validation Issues

If you believe a Codice Fiscale, Partita IVA, IBAN, municipality, or Belfiore code is being handled incorrectly, please provide:

1. The value being validated (anonymised if necessary)
2. The expected result
3. The actual result
4. Any relevant personal data or municipality information needed to reproduce the issue

## Development Setup

Clone the repository and build locally:

```powershell
dotnet build
dotnet test
```

To preview the documentation locally:

```powershell
docfx .\docfx.json --serve
```

## Pull Requests

Please keep pull requests focused on a single change whenever possible.

## Coding Guidelines

* Follow the existing coding style and project structure.
* Prefer readability over clever implementations.
* Avoid introducing external dependencies unless there is a strong justification.
* Keep all validation logic fully local. The library must not require external services or HTTP calls.

## Dataset Changes

Municipality and Belfiore data should be sourced from official or authoritative public datasets whenever possible.

When updating the dataset, please include the source and verify that the generated tests continue to pass.

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
