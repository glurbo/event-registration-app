namespace DTO.Public.Mappers;

public class RegistrationMapper : IMapper<Domain.Registration, Public.Registration>
{
    public Registration Map(Domain.Registration entity)
    {
        var res = new Public.Registration()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            IdentificationCode = entity.IdentificationCode
        };
        return res;
    }

    public Domain.Registration Map(Public.Registration entity)
    {
        var res = new Domain.Registration()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            IdentificationCode = entity.IdentificationCode
        };
        return res;
    }
}