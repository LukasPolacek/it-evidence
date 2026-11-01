using ItEvidence.Domain.Assets;
using ItEvidence.Domain.Employees;
using static ItEvidence.Domain.Tests.TestData;

namespace ItEvidence.Domain.Tests;

public class OffboardingTests
{
    [Fact]
    public void Assets_to_return_include_assets_in_repair_sorted_by_inventory_number()
    {
        var jan = NewEmployee("Jan");
        var eva = NewEmployee("Eva");
        var laptop = NewLaptop("IT-2026-0003");
        var phone = NewLaptop("IT-2026-0001");
        var evasMonitor = NewLaptop("IT-2026-0002");
        laptop.AssignTo(jan, Monday);
        phone.AssignTo(jan, Monday);
        evasMonitor.AssignTo(eva, Monday);
        phone.SendToRepair(Monday.AddDays(1));

        var toReturn = EmployeeOffboarding.AssetsToReturn(jan, [laptop, phone, evasMonitor, NewLaptop("IT-2026-0004")]);

        Assert.Equal(new[] { "IT-2026-0001", "IT-2026-0003" }, toReturn.Select(a => a.InventoryNumber));
    }

    [Fact]
    public void Offboarding_returns_all_assets_and_deactivates_employee()
    {
        var jan = NewEmployee("Jan");
        var eva = NewEmployee("Eva");
        var laptop = NewLaptop("IT-2026-0001");
        var phone = NewLaptop("IT-2026-0002");
        var evasLaptop = NewLaptop("IT-2026-0003");
        laptop.AssignTo(jan, Monday);
        phone.AssignTo(jan, Monday);
        evasLaptop.AssignTo(eva, Monday);
        phone.SendToRepair(Monday.AddDays(1));

        var result = EmployeeOffboarding.Offboard(jan, [laptop, phone, evasLaptop], Monday.AddDays(30));

        Assert.Equal(2, result.ReturnedAssets.Count);
        Assert.False(jan.IsActive);
        Assert.Equal(AssetStatus.InStock, laptop.Status);
        Assert.Equal(AssetStatus.InRepair, phone.Status);
        Assert.Null(phone.CurrentHolderId);
        Assert.Equal("Odchod zaměstnance", laptop.Assignments.Last().ReturnNote);
        // zarizeni jinych zamestnancu zustanou beze zmeny
        Assert.Equal(eva.Id, evasLaptop.CurrentHolderId);
    }

    [Fact]
    public void Employee_without_assets_is_just_deactivated()
    {
        var jan = NewEmployee();

        var result = EmployeeOffboarding.Offboard(jan, [], Monday);

        Assert.Empty(result.ReturnedAssets);
        Assert.False(jan.IsActive);
    }
}
