namespace SirenaTestAPI.Mappers
{
    public class RequestMapper
    {
        public static ExternalServices.ProviderOne.SearchRequest ToProviderOne(DTO.SearchRequest sourceRequest)
        {
            return new ExternalServices.ProviderOne.SearchRequest
            {
                DateFrom = sourceRequest.OriginDateTime,
                DateTo = sourceRequest.Filters?.DestinationDateTime,
                From = sourceRequest.Origin,
                To = sourceRequest.Destination,
                MaxPrice = sourceRequest.Filters?.MaxPrice
            };
        }

        public static ExternalServices.ProviderTwo.SearchRequest ToProviderTwo(DTO.SearchRequest sourceRequest)
        {
            return new ExternalServices.ProviderTwo.SearchRequest()
            {
                DepartureDate = sourceRequest.OriginDateTime,
                Departure = sourceRequest.Origin,
                Arrival = sourceRequest.Destination,
                MinTimeLimit = sourceRequest.Filters?.MinTimeLimit
            };
        }
    }
}
