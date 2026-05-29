namespace CodiceFiscale.Entities;

using Newtonsoft.Json;

/// <summary>
/// Represents a geographical coordinate with latitude and longitude.
/// </summary>
/// <param name="lat">Gets the latitude component of the coordinate.</param>
/// <param name="lng">Gets the longitude component of the coordinate.</param>
public record Coordinates(
    [property: JsonProperty("lat")] double lat,
    [property: JsonProperty("lng")] double lng);
