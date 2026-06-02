using CodiceFiscale.Entities;
using CodiceFiscale.Utils;

namespace CodiceFiscale;

/// <summary>
/// Provides extension methods and helpers for querying the Italian municipality dataset.
/// All lookups are performed against the embedded <c>municipalities.json</c> resource via <see cref="Utils.DataStore"/>.
/// </summary>
public static class MunicipalityExtensions
{
    /// <summary>
    /// Returns the municipality with the specified CAP (postal code / codice di avviamento postale).
    /// </summary>
    /// <param name="cap">The 5-digit CAP to search for (e.g. <c>"00186"</c>).</param>
    /// <returns>The matching <see cref="Municipality"/>, or <c>null</c> if not found.</returns>
    public static Municipality? GetMunicipalityByCAP(this string cap)
        => DataStore.Municipalities.FirstOrDefault(x => x.ZipCode.Equals(cap));

    /// <summary>
    /// Returns the municipality whose name matches the specified string (case-insensitive).
    /// </summary>
    /// <param name="name">The municipality name to search for (e.g. <c>"Roma"</c> or <c>"roma"</c>).</param>
    /// <returns>The matching <see cref="Municipality"/>, or <c>null</c> if not found.</returns>
    public static Municipality? GetMunicipalityByName(this string name)
        => name != null ? DataStore.Municipalities.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower())) : null;

    /// <summary>
    /// Returns the municipality with the specified Belfiore (cadastral) code.
    /// The comparison is case-sensitive; pass the code in the canonical uppercase form (e.g. <c>"H501"</c>).
    /// </summary>
    /// <param name="belfiore">The 4-character Belfiore code to search for.</param>
    /// <returns>The matching <see cref="Municipality"/>, or <c>null</c> if not found.</returns>
    public static Municipality? GetMunicipalityByBelfiore(this string belfiore)
        => belfiore != null ? DataStore.Municipalities.FirstOrDefault(x => x.CatastalCode.ToUpper().Equals(belfiore.ToUpper())) : null;

    /// <summary>
    /// Returns the municipality with the specified ISTAT code.
    /// </summary>
    /// <param name="code">The 6-digit ISTAT code to search for (e.g. <c>"058091"</c>).</param>
    /// <returns>The matching <see cref="Municipality"/>, or <c>null</c> if not found.</returns>
    public static Municipality? GetMunicipalityByCode(this string code)
        => DataStore.Municipalities.FirstOrDefault(x => x.Code.Equals(code));

    /// <summary>
    /// Returns all municipalities belonging to the specified province (case-insensitive).
    /// </summary>
    /// <param name="provinceName">The province name to filter by (e.g. <c>"Roma"</c>).</param>
    /// <returns>A sequence of <see cref="Municipality"/> objects in that province, or an empty sequence if none match.</returns>
    public static IEnumerable<Municipality>? GetAllByProvince(this string provinceName)
        => provinceName != null ? DataStore.Municipalities.Where(x => x.Province.Name.ToLower().Equals(provinceName.ToLower())) : null;

    /// <summary>
    /// Returns all municipalities in the embedded dataset (~7 896 comuni).
    /// </summary>
    /// <returns>The full collection of <see cref="Municipality"/> objects.</returns>
    public static IEnumerable<Municipality> GetAll()
        => DataStore.Municipalities;
}