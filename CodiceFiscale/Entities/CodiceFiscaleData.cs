using CodiceFiscale.Enums;

namespace CodiceFiscale.Entities;

/// <summary>
/// Represents the data required to generate or parse an Italian Codice Fiscale.
/// </summary>
/// <param name="Gender">The gender of the individual ("Male" for male, "Female" for female).</param>
/// <param name="DateOfBirth">The date of birth of the individual.</param>
/// <param name="BelfioreCode">The Belfiore code representing the place of birth.</param>
public record CodiceFiscaleData(
    Gender Gender,
    DateOnly DateOfBirth,
    string BelfioreCode);

