using System.Reflection;
using CodiceFiscale.Entities;
using Newtonsoft.Json;

namespace CodiceFiscale.Utils;

internal class JsonParser
{
    internal static IEnumerable<Municipality>? DeserializeMunicipalities()
        => DeserializeObject<Municipality>(Config.MunicipalitiesResourceName);

    internal static IEnumerable<ForeignCountry>? DeserializeForeignCountries()
        => DeserializeObject<ForeignCountry>(Config.ForeignCountriesResourceName);

    internal static IEnumerable<CheckCodeMap>? DeserializeCheckCodeMap()
        => DeserializeObject<CheckCodeMap>(Config.CheckCodeMapResourceName);

    private static IEnumerable<T>? DeserializeObject<T>(string jsonFile)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(jsonFile);
        if (stream is null) return null;
        using var reader = new StreamReader(stream);
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
    }
}
