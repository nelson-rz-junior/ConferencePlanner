using FrontEnd.Pages.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Admin;

public class EditSessionModel : PageModel
{
    private readonly IApiClient _apiClient;

    public EditSessionModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [BindProperty]
    public Session Session { get; set; }

    [TempData]
    public string Message { get; set; }

    public bool ShowMessage => !string.IsNullOrWhiteSpace(Message);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var session = await _apiClient.GetSessionAsync(id);

        Session = new Session
        {
            Id = session.Id,
            TrackId= session.TrackId,
            Title= session.Title,
            Abstract= session.Abstract,
            StartTime= session.StartTime,
            EndTime= session.EndTime
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _apiClient.PutSessionAsync(Session);

        Message = "Session updated successfully";

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var session = await _apiClient.GetSessionAsync(id);

        if (session != null)
        {
            await _apiClient.DeleteSessionAsync(id);
        }

        Message = "Session deleted successfully";

        return Page();
    }
}
