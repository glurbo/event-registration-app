namespace DTO.Public;

public class Event
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }

    public ICollection<Registration>? Registrations { get; set; }
}