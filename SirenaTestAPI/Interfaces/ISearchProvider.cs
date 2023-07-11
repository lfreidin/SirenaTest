using SirenaTestAPI.DTO;

namespace SirenaTestAPI.Interfaces
{
    public interface ISearchProvider
    {
        public ValueTask<List<DTO.Route>?> Search(SearchRequest searchRequest, CancellationToken cancellationToken);
        public Task<bool> CheckAvailability(CancellationToken cancellationToken);
    }
}
