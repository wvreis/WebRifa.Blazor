using Microsoft.AspNetCore.Identity;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Services.UserServices;

public class CustomUserIdProvider(
    IHttpContextAccessor context,
    IServiceScopeFactory scopeFactory) : ICustomUserIdProvider {
    
    private readonly IHttpContextAccessor _context = context;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    public async Task<Guid> GetUserIdAsync()
    {
        var user = _context?.HttpContext?.User;        
        if (user is null || !user!.Identity!.IsAuthenticated) {
            return Guid.Empty;
        }

        string userId = string.Empty;

        using (var scope = _scopeFactory.CreateScope()) {
            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            var user1 = await userManager.GetUserAsync(user);
            userId = await userManager.GetUserIdAsync(user1!);
        }

        Guid.TryParse(userId, out Guid result);
        return result;
    }
}
