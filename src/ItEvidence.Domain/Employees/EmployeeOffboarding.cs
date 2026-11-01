using ItEvidence.Domain.Assets;

namespace ItEvidence.Domain.Employees;

/// <summary>Vysledek odchodu zamestnance - seznam vracenych zarizeni.</summary>
public record OffboardingResult(IReadOnlyList<Asset> ReturnedAssets);

/// <summary>
/// Odchod zamestnance z firmy - co ma vratit a hromadne vraceni.
/// </summary>
public static class EmployeeOffboarding
{
    /// <summary>
    /// Zarizeni, ktera ma zamestnanec vratit - vsechna, jejichz je aktualnim drzitelem
    /// (vcetne zarizeni v servisu), serazena podle inventarniho cisla.
    /// </summary>
    public static IReadOnlyList<Asset> AssetsToReturn(Employee employee, IEnumerable<Asset> assets)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Vrati vsechna zarizeni zamestnance (s poznamkou "Odchod zamestnance")
    /// a zamestnance deaktivuje. Zarizeni ostatnich zamestnancu zustanou beze zmeny.
    /// </summary>
    public static OffboardingResult Offboard(Employee employee, IEnumerable<Asset> assets, DateTimeOffset at)
    {
        throw new NotImplementedException();
    }
}
