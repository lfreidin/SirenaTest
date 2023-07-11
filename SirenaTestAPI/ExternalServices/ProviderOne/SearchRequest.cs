namespace SirenaTestAPI.ExternalServices.ProviderOne;

// HTTP POST http://provider-one/api/v1/search

// HTTP GET http://provider-one/api/v1/ping
//      - HTTP 200 if provider is ready
//      - HTTP 500 if provider is down
public class SearchRequest: IEquatable<SearchRequest>
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public string From { get; set; }
    
    // Mandatory
    // End point of route, e.g. Sochi
    public string To { get; set; }
    
    // Mandatory
    // Start date of route
    public DateTime DateFrom { get; set; }
    
    // Optional
    // End date of route
    public DateTime? DateTo { get; set; }
    
    // Optional
    // Maximum price of route
    public decimal? MaxPrice { get; set; }

    public bool Equals(SearchRequest? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return From == other.From && To == other.To && DateFrom.Equals(other.DateFrom) && Nullable.Equals(DateTo, other.DateTo) && MaxPrice == other.MaxPrice;
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
        return HashCode.Combine(From, To, DateFrom, DateTo, MaxPrice);
    }
}

