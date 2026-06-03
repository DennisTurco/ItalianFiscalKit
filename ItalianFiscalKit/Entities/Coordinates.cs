namespace ItalianFiscalKit.Entities;

using Newtonsoft.Json;

/// <summary>
/// Represents a geographical coordinate with latitude and longitude.
/// </summary>
/// <param name="Lat">Gets the latitude component of the coordinate.</param>
/// <param name="Lng">Gets the longitude component of the coordinate.</param>
public record Coordinates(
    [property: JsonProperty("lat")] double Lat,
    [property: JsonProperty("lng")] double Lng);
