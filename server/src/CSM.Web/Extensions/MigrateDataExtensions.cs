using CSM.Infrastructure.Seeds;

namespace CSM.Web.Extensions;

internal static class MigrateDataExtensions
{
    internal static async Task UseMigrationDataAsync(this WebApplication app)
    {
        // To await every single Task run parallel
        await Task.WhenAll(
            app.SeedViewDataAsync()
        );
    }
}
