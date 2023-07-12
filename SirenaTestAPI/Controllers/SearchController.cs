using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SirenaTestAPI.ExternalServices.ProviderOne;
using SirenaTestAPI.ExternalServices.ProviderTwo;
using SirenaTestAPI.Interfaces;
using SirenaTestAPI.Services;
using SearchRequest = SirenaTestAPI.DTO.SearchRequest;

namespace SirenaTestAPI.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        // todo: use DI for providers and search service
        public SearchController(ILogger<SearchController> logger)
        {
            _searchService = new SearchService(new ISearchProvider[]
                { 
                    new ProviderOne(logger),
                    new ProviderTwo(logger)
                });
        }

        [HttpPost("search")]
        public async Task<ActionResult<SearchRequest>> Post(SearchRequest request, CancellationToken cancellationToken)
        {
            var result = await _searchService.SearchAsync(request, cancellationToken);
            if (result == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

        [HttpGet("ping")]
        public async Task<ActionResult> Get(CancellationToken cancellationToken)
        {
            return await _searchService.IsAvailableAsync(cancellationToken) ? Ok() : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }
}