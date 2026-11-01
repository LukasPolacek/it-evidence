using ItEvidence.Domain.Assets;
using ItEvidence.Domain.Employees;

namespace ItEvidence.Domain.Tests;

/// <summary>Pomocne hodnoty a tovarny pro testy.</summary>
internal static class TestData
{
    public static readonly DateTimeOffset Monday = new(2026, 11, 2, 9, 0, 0, TimeSpan.FromHours(1));
    public static readonly DateOnly Today = new(2026, 11, 2);

    public static Employee NewEmployee(string firstName = "Jan", string lastName = "Novák") =>
        new(Guid.NewGuid(), firstName, lastName, $"{firstName.ToLowerInvariant()}@firma.cz");

    public static Asset NewLaptop(string inventoryNumber = "IT-2026-0001", DateOnly? warrantyUntil = null) =>
        new(Guid.NewGuid(), inventoryNumber, AssetType.Laptop, "Lenovo", "ThinkPad T14",
            serialNumber: "PF3ABC12", purchaseDate: new DateOnly(2026, 1, 15), price: 28_990m,
            warrantyUntil: warrantyUntil ?? new DateOnly(2029, 1, 15));
}
