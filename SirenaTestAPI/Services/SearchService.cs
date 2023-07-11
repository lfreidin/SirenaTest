using System.Collections.Concurrent;
using SirenaTestAPI.ExternalServices.ProviderOne;
using SirenaTestAPI.ExternalServices.ProviderTwo;
using SirenaTestApi.Interfaces;
using SirenaTestAPI.Interfaces;
using SearchRequest = SirenaTestAPI.DTO.SearchRequest;
using SearchResponse = SirenaTestAPI.DTO.SearchResponse;

namespace SirenaTestAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchProvider[] _providers;
        private readonly ILogger _logger;

        public SearchService(ILogger logger)
        {
            _logger = logger;
            _providers = new ISearchProvider[]{ new ProviderOne(_logger), new ProviderTwo(_logger) };
        }

        public async Task<SearchResponse?> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>(_providers.Length);
            var allRoutes = new List<DTO.Route>();
            var errorMessage = string.Empty;
            foreach (var provider in _providers)
            {
                var routes = await provider.Search(request, cancellationToken);
                if (routes != null)
                {
                    allRoutes.AddRange(routes);
                }
            }

            if (!allRoutes.Any())
            {
                return null;
            }
            var durations = allRoutes.Select(route => (route.DestinationDateTime - route.OriginDateTime).TotalMinutes)
                .ToList();
            var prices = allRoutes.Select(route => route.Price).ToList();
            return new SearchResponse()
            {
                MaxMinutesRoute = (int)durations.Max(),
                MinMinutesRoute = (int)durations.Min(),
                MaxPrice = prices.Max(),
                MinPrice = prices.Min(),
                Routes = allRoutes.ToArray()
            };
        }

        public Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
