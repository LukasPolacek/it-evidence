namespace ItEvidence.Domain.Assets;

/// <summary>
/// Jedno prideleni zarizeni zamestnanci (zaznam v historii).
/// </summary>
public class AssetAssignment
{
    public AssetAssignment(Guid employeeId, DateTimeOffset assignedAt)
    {
        throw new NotImplementedException();
    }

    public Guid EmployeeId { get; }
    public DateTimeOffset AssignedAt { get; }

    /// <summary>Kdy bylo zarizeni vraceno, null = stale prideleno.</summary>
    public DateTimeOffset? ReturnedAt { get; private set; }

    /// <summary>Volitelna poznamka k vraceni (napr. "poskrabany displej").</summary>
    public string? ReturnNote { get; private set; }

    /// <summary>Prideleni trva (zarizeni jeste nebylo vraceno).</summary>
    public bool IsActive => throw new NotImplementedException();

    /// <summary>
    /// Ukonci prideleni. Cas vraceni nesmi byt driv nez cas prideleni
    /// a prideleni nesmi byt uz ukoncene - jinak <see cref="Common.DomainException"/>.
    /// </summary>
    public void Close(DateTimeOffset returnedAt, string? note)
    {
        throw new NotImplementedException();
    }
}
