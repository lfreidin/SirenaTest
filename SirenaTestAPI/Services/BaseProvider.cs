using System.Net;
using System.Text.Json;
using SirenaTestAPI.Interfaces;

namespace SirenaTestAPI.Services;

public abstract class BaseProvider<TRequest, TResponse, TRoute> : ISearchProvider 
    where TRequest : IEquatable<TRequest>
    where TResponse: IProviderSearchResponse<TRoute>
    where TRoute: IProviderRoute
{
    protected string baseApiUrl;
    protected HttpClient HttpClient = new();
    protected RoutesCache<TRequest, TRoute> Cache;
    private ILogger _logger;

    protected BaseProvider(string baseApiUrl, ILogger logger)
    {
        this.baseApiUrl = baseApiUrl;
        _logger = logger;
        Cache = new RoutesCache<TRequest, TRoute>();
    }

    public TRoute[]? GetFromCache(TRequest request)
    {
        var result = Cache.Get(request);
        return result;
    }

    public void AddToCache(TRequest request, TRoute[] routes)
    {
        Cache.Add(request,routes);
    }

    public abstract ValueTask<List<DTO.Route>?> Search(DTO.SearchRequest searchRequest, CancellationToken cancellationToken);

    public async Task<bool> CheckAvailability(CancellationToken cancellationToken)
    {
        try
        {
            var result = await HttpClient.GetAsync($"{baseApiUrl}/ping", cancellationToken);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                return false;
            }

            _logger.LogError($"Unknown status code in {GetType().FullName}.{nameof(CheckAvailability)}");
        }
        catch (Exception ex)
        {
            //todo: log exception
            return false;
        }
        return false;
    }

    protected async Task<TRoute[]?> GetRoutes(string requestUri, TRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await CheckAvailability(cancellationToken))
            {
                return null;
            }
            var response = await HttpClient.PostAsync(requestUri, JsonContent.Create(request), cancellationToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Unknown status code in {GetType().FullName}.{nameof(GetRoutes)}");
                return default;
            }

            var content =
                await response.Content.ReadFromJsonAsync<TResponse>(new JsonSerializerOptions(), cancellationToken);

            TRoute[]? routes;
            if (content == null)
            {
                _logger.LogError($"Empty content in {GetType().FullName}.{nameof(GetRoutes)}");
                _logger.LogInformation($"Locating routes in cache");
                routes = GetFromCache(request);
                if (routes == null)
                {
                    _logger.LogInformation($"Nothing found in cache");
                    return null;
                }
            }
            else
            {
                routes = content.Routes;
                AddToCache(request, routes);
            }

            return routes;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting routes for {GetType().FullName}", ex);
            return default;
        }
    }
}