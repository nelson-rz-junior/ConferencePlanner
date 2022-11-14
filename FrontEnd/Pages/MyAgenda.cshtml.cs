using ConferencePlanner.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Pages;

[Authorize]
public class MyAgendaModel : IndexModel
{
    public MyAgendaModel(ILogger<IndexModel> logger, IApiClient apiClient) : base(logger, apiClient)
    {
    }

    protected async override Task<List<SessionResponse>> GetSessionsAsync()
    {
        return await _apiClient.GetSessionsByAttendeeAsync(User.Identity.Name);
    }
}
