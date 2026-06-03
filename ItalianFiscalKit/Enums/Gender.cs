namespace ItalianFiscalKit.Enums;

/// <summary>
/// Specifies the biological gender of an individual, as encoded in the Italian Fiscal Code.
/// </summary>
public enum Gender : byte
{
    /// <summary>
    /// Represents a male individual. The day-of-birth field in the Fiscal Code ranges from 1 to 31.
    /// </summary>
    Male = 1,

    /// <summary>
    /// Represents a female individual. The day-of-birth field in the Fiscal Code ranges from 41 to 71 (actual day + 40).
    /// </summary>
    Female = 2
}
