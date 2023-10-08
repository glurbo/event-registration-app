namespace Domain.Base;

public class DomainEntity
{
    public Guid Id { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }
}