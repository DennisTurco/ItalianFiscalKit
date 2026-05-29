using Newtonsoft.Json;

namespace CodiceFiscale.Entities;

/// <summary>
/// Represents a foreign country with its catastal code, name, continent, and deletion status.
/// <para>CatastalCode: Gets the catastal code of the foreign country.</para>
/// <para>Name: Gets the name of the foreign country.</para>
/// <para>Continent: Gets the continent where the foreign country is located.</para>
/// <para>Deleted: Gets a value indicating whether the foreign country is deleted.</para>
/// </summary>
public record ForeignCountry(
    [property: JsonProperty("codiceCatastale")] string CatastalCode,
    [property: JsonProperty("nome")] string Name,
    [property: JsonProperty("continente")] string Continent,
    [property: JsonProperty("soppresso")] bool Deleted);
