using ConferencePlanner.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class SpeakersModel : PageModel
{
    private readonly IApiClient _apiClient;

    public SpeakersModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public IEnumerable<SpeakerResponse> Speakers { get; set; }

    public async Task OnGet()
    {
        var speakers = await _apiClient.GetSpeakersAsync();

        Speakers = speakers.OrderBy(s => s.Name);
    }
}
