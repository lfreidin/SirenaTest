using SirenaTestAPI.Mappers;

namespace SirenaTestAPI.ExternalServices.ProviderTwo
{
    public class RouteMapper 
    {
        public DTO.Route FromProviderRoute(Route sourceRoute)
        {
            return new DTO.Route()
            {
                Origin = sourceRoute.Departure.Point,
                Destination = sourceRoute.Arrival.Point,
                OriginDateTime = sourceRoute.Departure.Date,
                DestinationDateTime = sourceRoute.Departure.Date,
                TimeLimit = sourceRoute.TimeLimit,
                Price = sourceRoute.Price
            };
        }
    }
}
