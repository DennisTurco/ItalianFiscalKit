using Newtonsoft.Json;

namespace CodiceFiscale.Entities;

internal record CheckCodeMap(
    [property: JsonProperty("chars")] List<string> Chars,
    [property: JsonProperty("even")] int Even,
    [property: JsonProperty("odd")] int Odd);
