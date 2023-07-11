namespace SirenaTestAPI.Interfaces;

public interface IProviderSearchResponse<TRoute>
{
    TRoute[] Routes { get; set; }
}