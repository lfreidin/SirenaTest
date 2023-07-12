using SirenaTestAPI.ExternalServices.ProviderOne;
using SirenaTestAPI.Services;

namespace SirenaTestAPI.Tests
{
    public class RoutesCacheTests
    {
        private RoutesCache<SearchRequest, Route> _routeCache;
        private SearchRequest _request;

        [SetUp]
        public void Setup()
        {
            _routeCache = new RoutesCache<SearchRequest, Route>();
            _request = new SearchRequest()
            {
                DateFrom = new DateTime(2023, 5, 1, 0, 0, 0),
                DateTo = new DateTime(2023, 5, 2, 0, 0, 0),
                From = "Moscow",
                To = "Kemerovo",
                MaxPrice = 200000
            };
        }

        [Test]
        public void TestCacheWithValidRoutes()
        {
            var routes = new[]
            {
                new Route()
                {
                    DateFrom = new DateTime(2023, 5, 1, 12, 0, 0),
                    DateTo = new DateTime(2023, 5, 1, 16, 0, 0),
                    From = "Moscow",
                    To = "Kemerovo",
                    TimeLimit = DateTime.Now.AddMinutes(10)
                }
            };

            _routeCache.Add(_request, routes);
            var routes2 = _routeCache.Get(_request);
            Assert.AreEqual(1, routes2.Length);
        }

        [Test]
        public void TestCacheWithInvalidRoutes()
        {

            var routes = new[]
            {
                new Route()
                {
                    DateFrom = new DateTime(2023, 5, 1, 12, 0, 0),
                    DateTo = new DateTime(2023, 5, 1, 16, 0, 0),
                    From = "Moscow",
                    To = "Kemerovo",
                    TimeLimit = DateTime.Now.AddMinutes(-10)
                }
            };

            _routeCache.Add(_request, routes);
            var routes2 = _routeCache.Get(_request);

            Assert.IsFalse(routes2.Any());
        }
    }
}