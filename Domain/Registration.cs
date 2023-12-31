using Domain.Base;

namespace Domain;

public class Registration : DomainEntity
{
    public Guid EventId { get; set; }
    public Event? Event { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string IdentificationCode { get; set; } = default!;
}