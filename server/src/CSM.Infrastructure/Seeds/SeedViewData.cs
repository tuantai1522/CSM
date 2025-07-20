using CSM.Core.Features.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CSM.Infrastructure.Seeds;

public class SeedViewData(IViewRepository viewRepository, ILogger<SeedViewData> logger)
{
    public async Task SeedViewDataAsync()
    {
        try
        {
            await TrySeedViewDataAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[{Prefix}] Seeding error with message: {Message}",
                nameof(SeedViewData), ex.Message);
        }
    }

    private async Task TrySeedViewDataAsync()
    {
        var verifyExistedView = await viewRepository.VerifyExistedViewDataAsync(CancellationToken.None);

        if (!verifyExistedView)
        {
            // View for admin 
            
            // Main view
            var adminDashboard = View.CreateView("Admin dashboard", ViewCode.Dashboard, 1, "/admin");
            
            // Children view
            adminDashboard.AddViewChildren("User Management", ViewCode.UserManagement, 2, "/admin/user-management");
            adminDashboard.AddViewChildren("Role Management", ViewCode.RoleManagement, 3, "/admin/role-management");
            adminDashboard.AddViewChildren("Product Management", ViewCode.ProductManagement, 4, "/admin/product-management");
            adminDashboard.AddViewChildren("ExecutedEvent Management", ViewCode.ExecutedEventManagement, 5, "/admin/executed-event-management");
            
            // View for document 
            
            // Main view
            var document = View.CreateView("Document", ViewCode.Document, 6, "/document");
            
            // Children view
            document.AddViewChildren("Categories Document", ViewCode.CategoriesDocument, 7, "/document/categories");
            document.AddViewChildren("Finance Document", ViewCode.FinanceDocument, 8, "/document/finance");
            document.AddViewChildren("Product Management", ViewCode.ProductManagement, 9, "/admin/product-management");
            document.AddViewChildren("Inventory Document", ViewCode.InventoryDocument, 10, "/document/inventory");
            
            // View for report 
            
            // Main view
            var report = View.CreateView("Report", ViewCode.Report, 10, "/report");
            
            // Children view
            report.AddViewChildren("Meeting Report", ViewCode.MeetingReport, 11, "/report/meeting");
            report.AddViewChildren("Analytics Report", ViewCode.AnalyticsReport, 12, "/report/analytics");
            report.AddViewChildren("History Report", ViewCode.HistoryReport, 13, "/report/history");
            
            await viewRepository.AddViewsAsync([adminDashboard, document, report], CancellationToken.None);
            await viewRepository.UnitOfWork.SaveEntitiesAsync(CancellationToken.None);
        }
    }
}

public static class SeedViewDataExtensions
{
    public static async Task SeedViewDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<SeedViewData>();
        await initializer.SeedViewDataAsync();
    }
}