using ItEvidence.Domain.Employees;

namespace ItEvidence.Domain.Assets;

/// <summary>
/// Evidovane zarizeni (pocitac, monitor, telefon...).
/// Vsechna poruseni pravidel vyhazuji <see cref="Common.DomainException"/>.
/// </summary>
public class Asset
{
    /// <summary>
    /// Vytvori nove zarizeni ve stavu skladem.
    /// Pravidla: platne inventarni cislo (<see cref="InventoryNumbers.IsValid"/>),
    /// vyrobce a model povinne (okolni mezery se oriznou), cena nesmi byt zaporna,
    /// zaruka nesmi koncit pred datem nakupu (pokud jsou zadane obe hodnoty).
    /// </summary>
    public Asset(
        Guid id,
        string inventoryNumber,
        AssetType type,
        string manufacturer,
        string model,
        string? serialNumber = null,
        DateOnly? purchaseDate = null,
        decimal? price = null,
        DateOnly? warrantyUntil = null)
    {
        throw new NotImplementedException();
    }

    public Guid Id { get; }
    public string InventoryNumber { get; }
    public AssetType Type { get; }
    public string Manufacturer { get; }
    public string Model { get; }
    public string? SerialNumber { get; }
    public DateOnly? PurchaseDate { get; }
    public decimal? Price { get; }
    public DateOnly? WarrantyUntil { get; }

    /// <summary>Umisteni (kancelar, sklad...), null = neuvedeno.</summary>
    public string? Location { get; private set; }

    public AssetStatus Status { get; private set; }

    /// <summary>Aktualni drzitel, null = nikomu neprideleno.</summary>
    public Guid? CurrentHolderId => throw new NotImplementedException();

    /// <summary>Historie prideleni od nejstarsiho.</summary>
    public IReadOnlyList<AssetAssignment> Assignments => throw new NotImplementedException();

    public DateTimeOffset? RetiredAt { get; private set; }
    public string? RetirementReason { get; private set; }

    /// <summary>
    /// Prideli zarizeni zamestnanci. Zarizeni musi byt skladem a zamestnanec aktivni.
    /// Do historie prida nove prideleni a stav zmeni na Prideleno.
    /// </summary>
    public void AssignTo(Employee employee, DateTimeOffset at)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Vrati zarizeni od drzitele (ukonci aktivni prideleni).
    /// Povoleno ve stavu Prideleno (-> Skladem) a V servisu s drzitelem (stav zustava V servisu,
    /// po servisu pak zarizeni pujde na sklad). Bez drzitele vyhodi vyjimku.
    /// </summary>
    public void Return(DateTimeOffset at, string? note = null)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Odesle zarizeni do servisu. Povoleno ze stavu Skladem i Prideleno, drzitel zustava.
    /// </summary>
    public void SendToRepair(DateTimeOffset at)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Dokonci servis: ma-li zarizeni drzitele, stav bude Prideleno, jinak Skladem.
    /// Povoleno jen ve stavu V servisu.
    /// </summary>
    public void CompleteRepair(DateTimeOffset at)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Vyradi zarizeni. Povoleno jen ze stavu Skladem, duvod je povinny.
    /// Vyrazene zarizeni uz nelze pridelit, poslat do servisu, presunout ani znovu vyradit.
    /// </summary>
    public void Retire(DateTimeOffset at, string reason)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Zmeni umisteni. Prazdny text = umisteni neuvedeno (null). Nelze u vyrazeneho zarizeni.
    /// </summary>
    public void MoveTo(string? location)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Zaruka konci dnes nebo behem nasledujicich <paramref name="days"/> dni.
    /// Bez zadane zaruky nebo s jiz skoncenou zarukou vraci false.
    /// </summary>
    public bool IsWarrantyExpiringSoon(DateOnly today, int days = 30)
    {
        throw new NotImplementedException();
    }

    /// <summary>Zaruka uz skoncila (posledni den zaruky je pred dneskem).</summary>
    public bool IsWarrantyExpired(DateOnly today)
    {
        throw new NotImplementedException();
    }
}
