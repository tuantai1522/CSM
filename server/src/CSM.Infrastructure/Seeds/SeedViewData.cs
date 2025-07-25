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
            var adminDashboard = View.CreateView("Admin dashboard", ViewCode.Dashboard, 1, "/admin", 
            [
                View.CreateView("User Management", ViewCode.UserManagement, 2, "/admin/user-management", []),
                View.CreateView("Role Management", ViewCode.RoleManagement, 3, "/admin/role-management", []),
                View.CreateView("Product Management", ViewCode.ProductManagement, 4, "/admin/product-management", []),
                View.CreateView("ExecutedEvent Management", ViewCode.ExecutedEventManagement, 5, "/admin/executed-event-management", []),
            ]);
            
            // View for document 
            var document = View.CreateView("Document", ViewCode.Document, 6, "/document", 
            [
                View.CreateView("Categories Document", ViewCode.CategoriesDocument, 7, "/document/categories", []),
                View.CreateView("Finance Document", ViewCode.FinanceDocument, 8, "/document/finance", []),
                View.CreateView("Inventory Document", ViewCode.InventoryDocument, 9, "/document/inventory", []),
            ]);
            
            // View for report 
            var report = View.CreateView("Report", ViewCode.Report, 10, "/report", 
            [
                View.CreateView("Meeting Report", ViewCode.MeetingReport, 11, "/report/meeting", 
                [
                    View.CreateView("Meeting paper report", ViewCode.MeetingPaperReport, 12, "/report/meeting/paper", []),
                    View.CreateView("Meeting graph report", ViewCode.MeetingGraphReport, 13, "/report/meeting/graph", []),
                    View.CreateView("Meeting mail report", ViewCode.MeetingMailReport, 14, "/report/meeting/mail", [])
                ]),
                View.CreateView("Analytics Report", ViewCode.AnalyticsReport, 15, "/report/analytics", []),
                View.CreateView("History Report", ViewCode.HistoryReport, 16, "/report/history", []),
            ]);
            
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