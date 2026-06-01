namespace CodiceFiscale.Entities;

/// <summary>
/// Represents the result of a Codice Fiscale validation, including whether it is valid
/// and a list of human-readable error messages for each failed check.
/// </summary>
/// <param name="IsValid"><c>true</c> if all validation checks passed; otherwise, <c>false</c>.</param>
/// <param name="Errors">
/// A read-only list of error messages describing each failed validation step.
/// Empty when <see cref="IsValid"/> is <c>true</c>.
/// </param>
public record ValidationResult(bool IsValid, IReadOnlyList<string> Errors)
{
    /// <summary>Gets a <see cref="ValidationResult"/> representing a fully valid Codice Fiscale.</summary>
    public static readonly ValidationResult Valid = new(true, []);
}
