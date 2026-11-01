using ItEvidence.Domain.Assets;
using ItEvidence.Domain.Common;
using static ItEvidence.Domain.Tests.TestData;

namespace ItEvidence.Domain.Tests;

public class AssetLifecycleTests
{
    [Fact]
    public void Repair_of_stock_asset_returns_it_to_stock()
    {
        var asset = NewLaptop();

        asset.SendToRepair(Monday);
        Assert.Equal(AssetStatus.InRepair, asset.Status);

        asset.CompleteRepair(Monday.AddDays(5));
        Assert.Equal(AssetStatus.InStock, asset.Status);
    }

    [Fact]
    public void Assigned_asset_keeps_holder_during_repair_and_goes_back_to_him()
    {
        var asset = NewLaptop();
        var employee = NewEmployee();
        asset.AssignTo(employee, Monday);

        asset.SendToRepair(Monday.AddDays(1));
        Assert.Equal(AssetStatus.InRepair, asset.Status);
        Assert.Equal(employee.Id, asset.CurrentHolderId);

        asset.CompleteRepair(Monday.AddDays(5));
        Assert.Equal(AssetStatus.Assigned, asset.Status);
        Assert.Equal(employee.Id, asset.CurrentHolderId);
    }

    [Fact]
    public void Asset_returned_during_repair_goes_to_stock_after_repair()
    {
        var asset = NewLaptop();
        asset.AssignTo(NewEmployee(), Monday);
        asset.SendToRepair(Monday.AddDays(1));

        asset.Return(Monday.AddDays(2));
        Assert.Equal(AssetStatus.InRepair, asset.Status);
        Assert.Null(asset.CurrentHolderId);

        asset.CompleteRepair(Monday.AddDays(5));
        Assert.Equal(AssetStatus.InStock, asset.Status);
    }

    [Fact]
    public void Asset_in_repair_cannot_be_assigned()
    {
        var asset = NewLaptop();
        asset.SendToRepair(Monday);

        Assert.Throws<DomainException>(() => asset.AssignTo(NewEmployee(), Monday.AddHours(1)));
    }

    [Fact]
    public void Repair_cannot_be_completed_when_asset_is_not_in_repair()
    {
        Assert.Throws<DomainException>(() => NewLaptop().CompleteRepair(Monday));
    }

    [Fact]
    public void Stock_asset_can_be_retired_with_reason()
    {
        var asset = NewLaptop();

        asset.Retire(Monday, "Nefunkční základní deska");

        Assert.Equal(AssetStatus.Retired, asset.Status);
        Assert.Equal(Monday, asset.RetiredAt);
        Assert.Equal("Nefunkční základní deska", asset.RetirementReason);
    }

    [Fact]
    public void Retirement_requires_reason()
    {
        Assert.Throws<DomainException>(() => NewLaptop().Retire(Monday, "  "));
    }

    [Fact]
    public void Assigned_asset_cannot_be_retired()
    {
        var asset = NewLaptop();
        asset.AssignTo(NewEmployee(), Monday);

        Assert.Throws<DomainException>(() => asset.Retire(Monday.AddDays(1), "Starý"));
    }

    [Fact]
    public void Nothing_can_be_done_with_retired_asset()
    {
        var asset = NewLaptop();
        asset.Retire(Monday, "Starý");

        Assert.Throws<DomainException>(() => asset.AssignTo(NewEmployee(), Monday.AddDays(1)));
        Assert.Throws<DomainException>(() => asset.SendToRepair(Monday.AddDays(1)));
        Assert.Throws<DomainException>(() => asset.MoveTo("Sklad"));
        Assert.Throws<DomainException>(() => asset.Retire(Monday.AddDays(1), "Znovu"));
    }

    [Fact]
    public void Location_can_be_changed_and_cleared()
    {
        var asset = NewLaptop();

        asset.MoveTo("Kancelář 2.14");
        Assert.Equal("Kancelář 2.14", asset.Location);

        asset.MoveTo("  ");
        Assert.Null(asset.Location);
    }
}
