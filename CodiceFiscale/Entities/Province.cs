using Newtonsoft.Json;

namespace CodiceFiscale.Entities;

/// <summary>
/// Represents an Italian province with its name, acronym, code, and region.
/// </summary>
/// <param name="Name">The name of the province.</param>
/// <param name="Acronym">The acronym (sigla) of the province.</param>
/// <param name="Code">The code of the province.</param>
/// <param name="Region">The region to which the province belongs.</param>
public record Province (
    [property: JsonProperty("nome")] string Name,
    [property: JsonProperty("sigla")] string Acronym,
    [property: JsonProperty("codice")] string Code,
    [property: JsonProperty("regione")] string Region);
