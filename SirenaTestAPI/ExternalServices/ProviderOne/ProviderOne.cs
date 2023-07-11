using SirenaTestAPI.Mappers;
using SirenaTestAPI.Services;

namespace SirenaTestAPI.ExternalServices.ProviderOne
{
    public class ProviderOne : BaseProvider<SearchRequest, SearchResponse, Route>
    {
        public ProviderOne(ILogger logger) : base("http://provider-one/api/v1", logger)
        {
        }



        public override async ValueTask<List<DTO.Route>?> Search(DTO.SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            var request = RequestMapper.ToProviderOne(searchRequest);
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
                var requestUri = $"{baseApiUrl}/search";
                routes = await GetRoutes(requestUri, request, cancellationToken);
            }

            if (routes == null)
            {
                return null;
            }

            if (searchRequest.Filters?.MinTimeLimit != null)
            {
                routes = routes.Where(x => x.TimeLimit >= searchRequest.Filters.MinTimeLimit).ToArray();
            }

            return routes.Select(route => new RouteMapperOne().FromProviderRoute(route)).ToList();
        }
    }
}
