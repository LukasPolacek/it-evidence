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
        throw new NotImplementedException();
    }

    /// <summary>
    /// Overi tvar cisla: presne "IT-", ctyrmistny rok, "-", ctyrmistne poradi vetsi nez 0.
    /// </summary>
    public static bool IsValid(string? value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Dalsi volne cislo pro dany rok - o jedna vyssi nez nejvyssi existujici cislo
    /// tehoz roku (diry v rade se nezaplnuji). Cisla jinych roku a neplatna cisla se ignoruji.
    /// Pokud pro rok zadne cislo neni, zacina se od 0001.
    /// </summary>
    public static string Next(int year, IEnumerable<string> existingNumbers)
    {
        throw new NotImplementedException();
    }
}
