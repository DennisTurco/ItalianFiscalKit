using CodiceFiscale.Enums;

namespace CodiceFiscale.Entities;

/// <summary>
/// Represents the data required to generate or parse an Italian Codice Fiscale.
/// </summary>
public record CodiceFiscaleData(
    Gender Gender,
    DateTime DateOfBirth,
    string BelfioreCode,
    string CityOfBirth)
{
    /// <summary>
    /// The gender of the individual ("Male" for male, "Female" for female).
    /// </summary>
    public Gender Gender { get; init; } = Gender;

    /// <summary>
    /// The date of birth of the individual.
    /// </summary>
    public DateTime DateOfBirth { get; init; } = DateOfBirth;

    /// <summary>
    /// The Belfiore code representing the place of birth.
    /// </summary>
    public string BelfioreCode { get; init; } = BelfioreCode;

    /// <summary>
    /// The name of the city of birth.
    /// </summary>
    public string CityOfBirth { get; init; } = CityOfBirth;
}
