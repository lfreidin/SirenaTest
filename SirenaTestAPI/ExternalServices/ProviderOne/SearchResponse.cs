using SirenaTestAPI.Interfaces;

namespace SirenaTestAPI.ExternalServices.ProviderOne;

public class SearchResponse: IProviderSearchResponse<Route>
{
    // Mandatory
    // Array of routes
    public Route[] Routes { get; set; }
}

public class Route : IProviderRoute
{
    // Mandatory
    // Start point of route
    public string From { get; set; }

    // Mandatory
    // End point of route
    public string To { get; set; }

    // Mandatory
    // Start date of route
    public DateTime DateFrom { get; set; }

    // Mandatory
    // End date of route
    public DateTime DateTo { get; set; }

    // Mandatory
    // Price of route
    public decimal Price { get; set; }

    // Mandatory
    // Timelimit. After it expires, route became not actual
    public DateTime TimeLimit { get; set; }
    public bool IsValid => TimeLimit > DateTime.Now;
}