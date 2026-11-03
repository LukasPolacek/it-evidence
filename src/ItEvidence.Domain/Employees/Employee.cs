using ItEvidence.Domain.Common;

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
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("Jméno je povinné");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Příjmení je povinné");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new DomainException("Email je povinný");
        
        Id = id;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        IsActive = true;
    }

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    /// <summary>Cele jmeno ve tvaru "Jmeno Prijmeni".</summary>
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    /// <summary>Novy zamestnanec je aktivni, po deaktivaci uz aktivni neni.</summary>
    public bool IsActive { get; private set; }

    /// <summary>Deaktivace pri odchodu z firmy. Opakovana deaktivace nevadi.</summary>
    public void Deactivate()
    {
        IsActive = false;
    }
}
