using ItEvidence.Domain.Common;

namespace ItEvidence.Domain.Assets;

/// <summary>
/// Jedno prideleni zarizeni zamestnanci (zaznam v historii).
/// </summary>
public class AssetAssignment
{
    public AssetAssignment(Guid employeeId, DateTimeOffset assignedAt)
    {
        EmployeeId = employeeId;
        AssignedAt = assignedAt;
    }

    public Guid EmployeeId { get; }
    public DateTimeOffset AssignedAt { get; }

    /// <summary>Kdy bylo zarizeni vraceno, null = stale prideleno.</summary>
    public DateTimeOffset? ReturnedAt { get; private set; }

    /// <summary>Volitelna poznamka k vraceni (napr. "poskrabany displej").</summary>
    public string? ReturnNote { get; private set; }

    /// <summary>Prideleni trva (zarizeni jeste nebylo vraceno).</summary>
    public bool IsActive
    {
        get { return ReturnedAt == null; }
    }

    /// <summary>
    /// Ukonci prideleni. Cas vraceni nesmi byt driv nez cas prideleni
    /// a prideleni nesmi byt uz ukoncene - jinak <see cref="Common.DomainException"/>.
    /// </summary>
    public void Close(DateTimeOffset returnedAt, string? note)
    {
        if (!IsActive)
            throw new DomainException("Přiřazení už je uzavřené.");
        if (returnedAt < AssignedAt)
            throw new DomainException("Čas vrácení je dřív než čas přiřazení.");

        ReturnedAt = returnedAt;
        ReturnNote = note;
    }
}
