using FrontEnd.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FrontEnd.HealthChecks;

public class BackendHealthCheck : IHealthCheck
{
    private readonly IApiClient _apiClient;

    public BackendHealthCheck(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (await _apiClient.CheckHealthAsync())
        {
            return HealthCheckResult.Healthy();
        }

        return HealthCheckResult.Unhealthy();
    }
}
