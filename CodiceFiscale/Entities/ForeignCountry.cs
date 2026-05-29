using Newtonsoft.Json;

namespace CodiceFiscale.Entities;

/// <summary>
/// Represents a foreign country as encoded in the Italian Codice Fiscale dataset.
/// </summary>
/// <param name="CatastalCode">
/// The Belfiore catastal code assigned to the country (e.g. <c>Z614</c> for Venezuela).
/// All foreign country codes start with the letter <c>Z</c>.
/// </param>
/// <param name="Name">The Italian name of the country (e.g. <c>Venezuela</c>).</param>
/// <param name="Continent">The continent to which the country belongs (e.g. <c>America</c>).</param>
/// <param name="Deleted">
/// <c>true</c> if the country code has been abolished (e.g. former states such as Czechoslovakia);
/// <c>false</c> for currently active countries.
/// </param>
public record ForeignCountry(
    [property: JsonProperty("codiceCatastale")] string CatastalCode,
    [property: JsonProperty("nome")] string Name,
    [property: JsonProperty("continente")] string Continent,
    [property: JsonProperty("soppresso")] bool Deleted);
