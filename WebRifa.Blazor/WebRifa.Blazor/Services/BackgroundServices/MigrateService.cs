using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Services.BackgroundServices;

public class MigrateService(IServiceScopeFactory scopeFactory) : BackgroundService {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = scopeFactory.CreateScope()) {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
