namespace ItEvidence.Domain.Employees;

/// <summary>
/// Zamestnanec, kteremu lze pridelovat zarizeni a licence.
/// </summary>
public class Employee
{
    /// <summary>
    /// Vytvori aktivniho zamestnance.
    /// Jmeno i prijmeni jsou povinne (ne prazdne ani jen mezery), okolni mezery se oriznou.
    /// E-mail je povinny a musi obsahovat znak '@'.
    /// Pri poruseni pravidel vyhodi <see cref="Common.DomainException"/>.
    /// </summary>
    public Employee(Guid id, string firstName, string lastName, string email)
    {
        throw new NotImplementedException();
    }

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    /// <summary>Cele jmeno ve tvaru "Jmeno Prijmeni".</summary>
    public string FullName => throw new NotImplementedException();

    /// <summary>Novy zamestnanec je aktivni, po deaktivaci uz aktivni neni.</summary>
    public bool IsActive { get; private set; }

    /// <summary>Deaktivace pri odchodu z firmy. Opakovana deaktivace nevadi.</summary>
    public void Deactivate()
    {
        throw new NotImplementedException();
    }
}
