namespace CodiceFiscale;

/// <summary>
/// Provides normalization utilities for Italian Codice Fiscale strings.
/// </summary>
public static class CodiceFiscaleNormalizer
{
    // Positions (0-indexed) that hold digits and can be substituted with omocodia letters.
    // year[0]=6, year[1]=7, day[0]=9, day[1]=10, belfiore[1]=12, belfiore[2]=13, belfiore[3]=14
    private static readonly int[] OmocodePositions = [6, 7, 9, 10, 12, 13, 14];

    // Omocode letter -> digit character
    private static readonly Dictionary<char, char> OmocodeToDigit = new()
    {
        ['L'] = '0', ['M'] = '1', ['N'] = '2', ['P'] = '3', ['Q'] = '4',
        ['R'] = '5', ['S'] = '6', ['T'] = '7', ['U'] = '8', ['V'] = '9',
    };

    /// <summary>
    /// Normalizes a Codice Fiscale by converting any omocodia letter substitutions at
    /// numeric positions (6, 7, 9, 10, 12, 13, 14) back to their original digit values.
    /// Position 15 (check character) is never modified.
    /// </summary>
    /// <param name="cf">The Codice Fiscale string (may or may not contain omocodia substitutions).</param>
    /// <returns>
    /// The normalized Codice Fiscale with digits restored. Returns the input unchanged if it is
    /// null, empty, or not exactly 16 characters.
    /// </returns>
    public static string NormalizeOmocodia(string cf)
    {
        if (string.IsNullOrEmpty(cf) || cf.Length != 16)
            return cf;

        char[] chars = cf.ToUpperInvariant().ToCharArray();
        bool changed = false;

        foreach (int pos in OmocodePositions)
        {
            if (OmocodeToDigit.TryGetValue(chars[pos], out char digit))
            {
                chars[pos] = digit;
                changed = true;
            }
        }

        return changed ? new string(chars) : cf.ToUpperInvariant();
    }

    /// <summary>
    /// Determines whether a Codice Fiscale contains omocodia letter substitutions at any
    /// numeric position.
    /// </summary>
    /// <param name="cf">The Codice Fiscale to inspect.</param>
    /// <returns><c>true</c> if any omocodia substitution is present; otherwise, <c>false</c>.</returns>
    public static bool IsOmocodia(string cf)
    {
        if (string.IsNullOrEmpty(cf) || cf.Length != 16)
            return false;

        string upper = cf.ToUpperInvariant();
        return OmocodePositions.Any(pos => OmocodeToDigit.ContainsKey(upper[pos]));
    }
}
