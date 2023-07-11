using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SirenaTestAPI.DTO;
using SirenaTestApi.Interfaces;
using SirenaTestAPI.Services;

namespace SirenaTestAPI.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchService _searchService;

        // todo: use DI for searchService
        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
            _searchService = new SearchService(_logger);
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
        public ActionResult Get()
        {
            var available = new Random().Next(0, 2)==1;
            return available ? Ok() : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }
}