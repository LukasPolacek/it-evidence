using ItEvidence.Domain.Common;
using ItEvidence.Domain.Employees;

namespace ItEvidence.Domain.Tests;

public class EmployeeTests
{
    [Fact]
    public void New_employee_is_active_and_has_full_name()
    {
        var employee = new Employee(Guid.NewGuid(), "  Jan ", " Novák", "jan.novak@firma.cz");

        Assert.True(employee.IsActive);
        Assert.Equal("Jan", employee.FirstName);
        Assert.Equal("Novák", employee.LastName);
        Assert.Equal("Jan Novák", employee.FullName);
    }

    [Theory]
    [InlineData("", "Novák", "jan@firma.cz")]
    [InlineData("Jan", "   ", "jan@firma.cz")]
    [InlineData("Jan", "Novák", "")]
    [InlineData("Jan", "Novák", "jan.firma.cz")]
    public void Invalid_employee_data_is_rejected(string firstName, string lastName, string email)
    {
        Assert.Throws<DomainException>(() => new Employee(Guid.NewGuid(), firstName, lastName, email));
    }

    [Fact]
    public void Deactivated_employee_is_not_active_and_deactivation_can_repeat()
    {
        var employee = TestData.NewEmployee();

        employee.Deactivate();
        employee.Deactivate();

        Assert.False(employee.IsActive);
    }
}
