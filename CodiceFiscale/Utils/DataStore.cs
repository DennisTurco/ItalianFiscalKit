using ItalianFiscalKit.Entities;

namespace ItalianFiscalKit.Utils;

internal static class DataStore
{
    private static readonly Lazy<IReadOnlyList<Municipality>> _municipalities =
        new (() => JsonParser.DeserializeMunicipalities()?.ToList().AsReadOnly()
            ?? throw new InvalidOperationException("municipalities.json not found"));

    private static readonly Lazy<IReadOnlyList<ForeignCountry>> _foreignCountry =
        new (() => JsonParser.DeserializeForeignCountries()?.ToList().AsReadOnly()
            ?? throw new InvalidOperationException("foreign_countries.json not found"));

    private static readonly Lazy<IReadOnlyList<CheckCodeMap>> _checkCodeMap =
        new (() => JsonParser.DeserializeCheckCodeMap()?.ToList().AsReadOnly()
            ?? throw new InvalidOperationException("check_code_map.json not found"));

    internal static IReadOnlyList<Municipality> Municipalities => _municipalities.Value;
    internal static IReadOnlyList<ForeignCountry> ForeignCountries => _foreignCountry.Value;
    internal static IReadOnlyList<CheckCodeMap> CheckCodeMap => _checkCodeMap.Value;
}
