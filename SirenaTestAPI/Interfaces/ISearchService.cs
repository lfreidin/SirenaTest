﻿using SirenaTestAPI.DTO;

namespace SirenaTestAPI.Interfaces;

public interface ISearchService
{
    public Task<SearchResponse?> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}


