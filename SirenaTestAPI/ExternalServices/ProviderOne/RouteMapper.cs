using SirenaTestAPI.Mappers;

namespace SirenaTestAPI.ExternalServices.ProviderOne;

public class RouteMapper 
{
    public DTO.Route FromProviderRoute(Route sourceRoute)
    {
        return new DTO.Route()
        {
            Origin = sourceRoute.From,
            Destination = sourceRoute.To,
            OriginDateTime = sourceRoute.DateFrom,
            DestinationDateTime = sourceRoute.DateTo,
            TimeLimit = sourceRoute.TimeLimit,
            Price = sourceRoute.Price,
            Id = Guid.NewGuid()
        };
    }
}