namespace ItEvidence.Domain.Assets;

public enum AssetStatus
{
    /// <summary>Skladem - lze pridelit.</summary>
    InStock,
    /// <summary>Prideleno zamestnanci.</summary>
    Assigned,
    /// <summary>V servisu - drzitel (pokud byl) zustava.</summary>
    InRepair,
    /// <summary>Vyrazeno - konecny stav.</summary>
    Retired
}
