using SirenaTestAPI.Mappers;
using SirenaTestAPI.Services;

namespace SirenaTestAPI.ExternalServices.ProviderTwo
{
    public class ProviderTwo : BaseProvider<SearchRequest, SearchResponse, Route>
    {
        public ProviderTwo(ILogger logger) : base("http://provider-two/api/v1", logger)
        {
        }

        public override async ValueTask<List<DTO.Route>?> Search(DTO.SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            var request = RequestMapper.ToProviderTwo(searchRequest);
            Route[]? routes;
            if (searchRequest.Filters?.OnlyCached != false)
            {
                routes = GetFromCache(request);
                if (routes == null)
                {
                    return null;
                }
            }
            else
            {
                var requestUri = $"{BaseApiUrl}/search";
                routes = await GetRoutes(requestUri, request, cancellationToken);
            }

            if (routes == null)
            {
                return null;
            }

            if (searchRequest.Filters?.MaxPrice != null)
            {
                routes = routes.Where(route => route.Price <= searchRequest.Filters.MaxPrice).ToArray();
            }

            if (searchRequest.Filters?.DestinationDateTime != null)
            {
                routes = routes.Where(route => route.Arrival.Date <= searchRequest.Filters.DestinationDateTime.Value)
                    .ToArray();
            }

            return routes.Select(route => new RouteMapper().FromProviderRoute(route)).ToList();
        }
    }
}
