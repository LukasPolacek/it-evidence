namespace ItEvidence.Domain.Common;

/// <summary>
/// Poruseni pravidla domeny (napr. prideleni zarizeni, ktere neni skladem).
/// Zprava je urcena pro uzivatele, proto je cesky.
/// </summary>
public class DomainException(string message) : Exception(message);
