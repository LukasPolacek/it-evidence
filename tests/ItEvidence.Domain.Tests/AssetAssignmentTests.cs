using ItEvidence.Domain.Assets;
using ItEvidence.Domain.Common;
using static ItEvidence.Domain.Tests.TestData;

namespace ItEvidence.Domain.Tests;

public class AssetAssignmentTests
{
    [Fact]
    public void Assigning_sets_holder_status_and_history()
    {
        var asset = NewLaptop();
        var employee = NewEmployee();

        asset.AssignTo(employee, Monday);

        Assert.Equal(AssetStatus.Assigned, asset.Status);
        Assert.Equal(employee.Id, asset.CurrentHolderId);
        var assignment = Assert.Single(asset.Assignments);
        Assert.Equal(employee.Id, assignment.EmployeeId);
        Assert.Equal(Monday, assignment.AssignedAt);
        Assert.True(assignment.IsActive);
    }

    [Fact]
    public void Assigned_asset_cannot_be_assigned_again()
    {
        var asset = NewLaptop();
        asset.AssignTo(NewEmployee("Jan"), Monday);

        Assert.Throws<DomainException>(() => asset.AssignTo(NewEmployee("Eva"), Monday.AddHours(1)));
    }

    [Fact]
    public void Asset_cannot_be_assigned_to_inactive_employee()
    {
        var employee = NewEmployee();
        employee.Deactivate();

        Assert.Throws<DomainException>(() => NewLaptop().AssignTo(employee, Monday));
    }

    [Fact]
    public void Returning_closes_assignment_and_puts_asset_back_to_stock()
    {
        var asset = NewLaptop();
        asset.AssignTo(NewEmployee(), Monday);

        asset.Return(Monday.AddDays(30), "poškrábaný displej");

        Assert.Equal(AssetStatus.InStock, asset.Status);
        Assert.Null(asset.CurrentHolderId);
        var assignment = Assert.Single(asset.Assignments);
        Assert.False(assignment.IsActive);
        Assert.Equal(Monday.AddDays(30), assignment.ReturnedAt);
        Assert.Equal("poškrábaný displej", assignment.ReturnNote);
    }

    [Fact]
    public void Return_cannot_be_earlier_than_assignment()
    {
        var asset = NewLaptop();
        asset.AssignTo(NewEmployee(), Monday);

        Assert.Throws<DomainException>(() => asset.Return(Monday.AddMinutes(-1)));
    }

    [Fact]
    public void Asset_without_holder_cannot_be_returned()
    {
        Assert.Throws<DomainException>(() => NewLaptop().Return(Monday));
    }

    [Fact]
    public void History_keeps_all_assignments_in_order()
    {
        var asset = NewLaptop();
        var jan = NewEmployee("Jan");
        var eva = NewEmployee("Eva");

        asset.AssignTo(jan, Monday);
        asset.Return(Monday.AddDays(10));
        asset.AssignTo(eva, Monday.AddDays(11));

        Assert.Equal(new[] { jan.Id, eva.Id }, asset.Assignments.Select(a => a.EmployeeId));
        Assert.False(asset.Assignments[0].IsActive);
        Assert.True(asset.Assignments[1].IsActive);
        Assert.Equal(eva.Id, asset.CurrentHolderId);
    }

    [Fact]
    public void Closed_assignment_cannot_be_closed_again()
    {
        var assignment = new AssetAssignment(Guid.NewGuid(), Monday);
        assignment.Close(Monday.AddDays(1), null);

        Assert.Throws<DomainException>(() => assignment.Close(Monday.AddDays(2), null));
    }
}
