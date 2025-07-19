using CSM.Core.Features.Roles;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CSM.Infrastructure.Seeds;

public class SeedRoleData(IRoleRepository roleRepository, ILogger<SeedRoleData> logger)
{
    public async Task SeedRoleDataAsync()
    {
        try
        {
            await TrySeedRoleData();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[{Prefix}] Seeding error with message: {Message}",
                nameof(SeedRoleData), ex.Message);
        }
    }

    private async Task TrySeedRoleData()
    {
        var verifyExistedRole = await roleRepository.VerifyExistedRoleDataAsync(CancellationToken.None);

        if (!verifyExistedRole)
        {
            var role = Role.CreateRole("Administrator", "R001", "To manage system and users");
            
            await roleRepository.AddRoleAsync(role, CancellationToken.None);
            await roleRepository.UnitOfWork.SaveEntitiesAsync(CancellationToken.None);
        }
    }
}

public static class SeedRoleDataExtensions
{
    public static async Task SeedRoleDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<SeedRoleData>();
        await initializer.SeedRoleDataAsync();
    }
}