using SirenaTestAPI.Interfaces;

namespace SirenaTestAPI.ExternalServices.ProviderTwo
{
    public class SearchResponse: IProviderSearchResponse<Route>
    {
        // Mandatory
        // Array of routes
        public Route[] Routes { get; set; }
    }

    public class Route: IProviderRoute
    {
        // Mandatory
        // Start point of route
        public RoutePoint Departure { get; set; }


        // Mandatory
        // End point of route
        public RoutePoint Arrival { get; set; }

        // Mandatory
        // Price of route
        public decimal Price { get; set; }

        // Mandatory
        // Timelimit. After it expires, route became not actual
        public DateTime TimeLimit { get; set; }
        public bool IsValid => TimeLimit > DateTime.Now;
    }

    public class RoutePoint
    {
        // Mandatory
        // Name of point, e.g. Moscow\Sochi
        public string Point { get; set; }

        // Mandatory
        // Date for point in Route, e.g. Point = Moscow, Date = 2023-01-01 15-00-00
        public DateTime Date { get; set; }
    }
}
