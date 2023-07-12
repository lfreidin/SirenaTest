using SirenaTestAPI.Interfaces;
using SearchRequest = SirenaTestAPI.DTO.SearchRequest;
using SearchResponse = SirenaTestAPI.DTO.SearchResponse;

namespace SirenaTestAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchProvider[] _providers;

        public SearchService(ISearchProvider[] providers)
        {
            _providers = providers;
        }

        public async Task<SearchResponse?> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            var allRoutes = new List<DTO.Route>();
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

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            // Критерии не определены
            // Я бы предлоложил, что если в кэше ничего нет, и все провайдеры не доступны,
            // то сервис тоже можно считать недоступным
            // если не провайдеры доступны, то кэш непустой, то можно было бы выводить доступные данные
            // если часть провайдеров доступно, неясно, будут ли частичные результаты валидными или лучше сообщить о недоступности
            return new Random().Next(0, 2) == 1;
        }
    }
}
