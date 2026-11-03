using System.Text.RegularExpressions;

namespace ItEvidence.Domain.Assets;

/// <summary>
/// Inventarni cisla ve tvaru IT-RRRR-NNNN (napr. IT-2026-0001).
/// Poradi se cisluje zvlast pro kazdy rok.
/// </summary>
public static class InventoryNumbers
{
    /// <summary>
    /// Sestavi cislo z roku a poradi, napr. (2026, 7) -> "IT-2026-0007".
    /// Rok musi byt 2000-2099, poradi 1-9999, jinak <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    public static string Format(int year, int sequence)
    {
        if ((year < 2000) || (year > 2099))
            throw new ArgumentOutOfRangeException(nameof(year));
        if ((sequence < 1) || (sequence > 9999))
            throw new ArgumentOutOfRangeException(nameof(sequence));
        return $"IT-{year}-{sequence:D4}";
    }

    /// <summary>
    /// Overi tvar cisla: presne "IT-", ctyrmistny rok, "-", ctyrmistne poradi vetsi nez 0.
    /// </summary>
    public static bool IsValid(string? value)
    {
        if (value == null)
            return false;
        if (value.EndsWith("-0000"))
            return false;
        if (string.IsNullOrEmpty(value))
            return false;
        if (!Regex.IsMatch(value, @"^IT-\d{4}-\d{4}$"))
            return false;
        return true;
    }

    /// <summary>
    /// Dalsi volne cislo pro dany rok - o jedna vyssi nez nejvyssi existujici cislo
    /// tehoz roku (diry v rade se nezaplnuji). Cisla jinych roku a neplatna cisla se ignoruji.
    /// Pokud pro rok zadne cislo neni, zacina se od 0001.
    /// </summary>
    public static string Next(int year, IEnumerable<string> existingNumbers)
    {
        int highest = 0;               
        string prefix = $"IT-{year}-"; 
        foreach (string number in existingNumbers)
        {
            if (!IsValid(number) || !number.StartsWith(prefix))
                continue;

            int sequence = int.Parse(number.Substring(8)); 
            if (sequence > highest)
                highest = sequence;
        }
        return Format(year, highest + 1);
    }
}
