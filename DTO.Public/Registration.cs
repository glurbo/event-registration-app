namespace DTO.Public;

public class Registration
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string IdentificationCode { get; set; } = default!;
}