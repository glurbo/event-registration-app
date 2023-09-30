using Domain.Base;

namespace Domain;

public class Event : DomainEntity
{
    public string Name { get; set; } = default!;
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }

    public ICollection<Registration>? Registrations { get; set; }
}