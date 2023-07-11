using System.Collections.Concurrent;
using SirenaTestAPI.Interfaces;

namespace SirenaTestAPI.Services
{
    public class RoutesCache<TRequest, TRoute>
        where TRequest : IEquatable<TRequest>
        where TRoute : IProviderRoute
    {
        private static readonly ConcurrentDictionary<TRequest, TRoute[]> RoutesDict = new();

        public TRoute[]? Get(TRequest request)
        {
            if (!RoutesDict.TryGetValue(request, out var routes))
            {
                return null;
            }
            routes = routes.Where(x => x.IsValid).ToArray();
            RoutesDict[request] = routes;
            return routes;
        }

        public void Add(TRequest request, TRoute[] routes)
        {
            RoutesDict.AddOrUpdate(request, routes, (_, _) => routes);
        }
    }
}
