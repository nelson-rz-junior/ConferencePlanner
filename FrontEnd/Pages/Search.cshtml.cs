using ConferencePlanner.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class SearchModel : PageModel
{
    private readonly IApiClient _apiClient;

    public SearchModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public string Term { get; set; } = string.Empty;

    public List<SearchResult> SearchResults { get; set; } = new();

    public async Task OnGetAsync(string term)
    {
        Term = term;

        if (!string.IsNullOrWhiteSpace(term))
        {
            SearchResults = await _apiClient.SearchAsync(term);
        }
    }
}
