namespace SirenaTestAPI.ExternalServices.ProviderTwo;

// HTTP POST http://provider-two/api/v1/search

// HTTP GET http://provider-two/api/v1/ping
//      - HTTP 200 if provider is ready
//      - HTTP 500 if provider is down
//

public class SearchRequest: IEquatable<SearchRequest>
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public string Departure { get; set; }
    
    // Mandatory
    // End point of route, e.g. Sochi
    public string Arrival { get; set; }
    
    // Mandatory
    // Start date of route
    public DateTime DepartureDate { get; set; }
    
    // Optional
    // Minimum value of timelimit for route
    public DateTime? MinTimeLimit { get; set; }

    public bool Equals(SearchRequest? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Departure == other.Departure && Arrival == other.Arrival && DepartureDate.Equals(other.DepartureDate) && Nullable.Equals(MinTimeLimit, other.MinTimeLimit);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SearchRequest)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Departure, Arrival, DepartureDate, MinTimeLimit);
    }
}

