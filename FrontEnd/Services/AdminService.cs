using FrontEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Services;

public class AdminService : IAdminService
{
    private readonly IServiceProvider _serviceProvider;

    private bool _adminExists;

    public AdminService(IServiceProvider serviceProvider)
	{
        _serviceProvider = serviceProvider;
	}

	public async Task<bool> AllowAdminUserCreationAsync()
	{
		if (_adminExists)
		{
			return false;
		}
		else
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();

                if (await db.Users.AnyAsync(user => user.IsAdmin))
                {
                    // There are already admin users so disable admin creation
                    _adminExists = true;
                    return false;
                }

                // There are no admin users so enable admin creation
                return true;
            }
		}
	}
}
