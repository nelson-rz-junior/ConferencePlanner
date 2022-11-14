using ConferencePlanner.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrontEnd.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    protected readonly IApiClient _apiClient;

    public IndexModel(ILogger<IndexModel> logger, IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public IEnumerable<IGrouping<DateTimeOffset?, SessionResponse>> Sessions { get; set; } = null!;

    public IEnumerable<(int Offset, DayOfWeek? DayofWeek)> DayOffsets { get; set; } = null!;

    public int CurrentDayOffset { get; set; }

    public bool IsAdmin { get; set; }

    [TempData]
    public string Message { get; set; }

    public bool ShowMessage => !string.IsNullOrWhiteSpace(Message);

    public List<int> UserSessions { get; set; } = new List<int>();

    protected virtual Task<List<SessionResponse>> GetSessionsAsync()
    {
        return _apiClient.GetSessionsAsync();
    }

    public async Task OnGetAsync(int day = 0)
    {
        CurrentDayOffset= day;
        IsAdmin = User.IsAdmin();

        if (User.Identity.IsAuthenticated)
        {
            var userSessions = await _apiClient.GetSessionsByAttendeeAsync(User.Identity.Name);
            UserSessions = userSessions.Select(s => s.Id).ToList();
        }

        var sessions = await GetSessionsAsync();
        var startDate = sessions.Min(s => s.StartTime?.Date);

        DayOffsets = sessions.Select(s => s.StartTime?.Date)
            .Distinct()
            .OrderBy(d => d)
            .Select(day => ((int)Math.Floor((day!.Value - startDate)?.TotalDays ?? 0), day?.DayOfWeek))
            .ToList();

        var filterDate = startDate?.AddDays(day);

        Sessions = sessions.Where(s => s.StartTime?.Date == filterDate)
            .OrderBy(s => s.TrackId)
            .GroupBy(s => s.StartTime)
            .OrderBy(g => g.Key);
    }

    public async Task<IActionResult> OnPostAsync(int sessionId)
    {
        await _apiClient.AddSessionToAttendeeAsync(User.Identity.Name, sessionId);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRemoveAsync(int sessionId)
    {
        await _apiClient.RemoveSessionFromAttendeeAsync(User.Identity.Name, sessionId);

        return RedirectToPage();
    }
}