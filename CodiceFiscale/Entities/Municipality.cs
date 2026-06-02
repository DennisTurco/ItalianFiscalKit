using Newtonsoft.Json;

namespace CodiceFiscale.Entities;

/// <summary>
/// Represents an Italian municipality with related administrative and contact information.
/// </summary>
public record Municipality
{
    /// <summary>
    /// Gets the unique code of the municipality.
    /// </summary>
    [JsonProperty("codice")]
    public required string Code { get; init; }

    /// <summary>
    /// Gets the name of the municipality.
    /// </summary>
    [JsonProperty("nome")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the foreign name of the municipality, if available.
    /// </summary>
    [JsonProperty("nomeStraniero")]
    public string? ForeignName { get; init; }

    /// <summary>
    /// Gets the catastal code of the municipality.
    /// </summary>
    [JsonProperty("codiceCatastale")]
    public required string CatastalCode { get; init; }

    /// <summary>
    /// Gets the ZIP code of the municipality.
    /// </summary>
    [JsonProperty("cap")]
    public required string ZipCode { get; init; }

    /// <summary>
    /// Gets the telephone prefix of the municipality.
    /// </summary>
    [JsonProperty("prefisso")]
    public string? Prefix { get; init; }

    /// <summary>
    /// Gets the province to which the municipality belongs.
    /// </summary>
    [JsonProperty("provincia")]
    public required Province Province { get; init; }

    /// <summary>
    /// Gets the email address of the municipality.
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; init; }

    /// <summary>
    /// Gets the certified email (PEC) address of the municipality.
    /// </summary>
    [JsonProperty("pec")]
    public string? PEC { get; init; }

    /// <summary>
    /// Gets the telephone number of the municipality.
    /// </summary>
    [JsonProperty("telefono")]
    public string? Telephone { get; init; }

    /// <summary>
    /// Gets the fax number of the municipality.
    /// </summary>
    [JsonProperty("fax")]
    public string? FAX { get; init; }

    /// <summary>
    /// Gets the geographical coordinates of the municipality.
    /// </summary>
    [JsonProperty("coordinate")]
    public required Coordinates Coordinates { get; init; }
}
