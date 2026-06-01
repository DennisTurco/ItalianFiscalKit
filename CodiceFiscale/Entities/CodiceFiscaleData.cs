using CodiceFiscale.Enums;

namespace CodiceFiscale.Entities;

/// <summary> Represents the personal data extracted from or used to generate an Italian Codice Fiscale.
/// </summary>
/// <param name="Gender">The gender of the individual.</param>
/// <param name="DateOfBirth">The date of birth of the individual.</param>
/// <param name="BelfioreCode">The 4-character Belfiore (cadastral) code representing the place of birth.</param>
public record CodiceFiscaleData(
    Gender Gender,
    DateOnly DateOfBirth,
    string BelfioreCode);

