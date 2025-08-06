using DishesAPI.DbContexts;
using DishesMinimalApi.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace DishesMinimalApi.HostedServices;

public class DbContextInitializer(IServiceScopeFactory _serviceScopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // apply peneding migrations (schema) based on DbContext

        using var scope = _serviceScopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<DishesDbContext>();

        await dbContext.Database.MigrateAsync(cancellationToken);
   
        //seed data using seeder
        var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
        await seeder.SeedAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
