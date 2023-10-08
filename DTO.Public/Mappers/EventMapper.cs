namespace DTO.Public.Mappers;

public class EventMapper : IMapper<Domain.Event, Public.Event>
{
    private readonly RegistrationMapper _registrationMapper = new RegistrationMapper();

    public Event Map(Domain.Event entity)
    {
        var registrations = entity.Registrations?.Select(x => _registrationMapper.Map(x)!).ToList();
        var res = new Public.Event()
        {
            Id = entity.Id.ToString(),
            Date = entity.Date,
            MaxParticipants = entity.MaxParticipants,
            Name = entity.Name,
            Registrations = registrations
        };
        return res;
    }

    public Domain.Event Map(Event entity)
    {
        var registrations = entity.Registrations?.Select(x => _registrationMapper.Map(x)!).ToList();
        var res = new Domain.Event()
        {
            Id = Guid.Parse(entity.Id),
            Date = entity.Date,
            MaxParticipants = entity.MaxParticipants,
            Name = entity.Name,
            Registrations = registrations
        };
        return res;
    }
}