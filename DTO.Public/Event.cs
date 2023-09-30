namespace DTO.Public;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }

    public ICollection<Registration>? Registrations { get; set; }
}