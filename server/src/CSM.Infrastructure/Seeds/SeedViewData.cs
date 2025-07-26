using CSM.Core.Features.Roles;
using CSM.Core.Features.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CSM.Infrastructure.Seeds;

public class SeedViewData(IViewRepository viewRepository, IRoleRepository roleRepository, ILogger<SeedViewData> logger)
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
        // View for admin 
        var userManagement = View.CreateView("User Management", ViewCode.UserManagement, 2, "/admin/user-management", 63, []);
        var roleManagement = View.CreateView("Role Management", ViewCode.RoleManagement, 3, "/admin/role-management", 63, []);
        var productManagement = View.CreateView("Product Management", ViewCode.ProductManagement, 4, "/admin/product-management", 63, []);
        var executedEventManagement = View.CreateView("ExecutedEvent Management", ViewCode.ExecutedEventManagement, 5, "/admin/executed-event-management", 63, []);
        
        var adminDashboard = View.CreateView("Admin dashboard", ViewCode.Dashboard, 1, "/admin", 0,
        [
            userManagement,
            roleManagement,
            productManagement,
            executedEventManagement,
        ]);
        
        // View for document 
        var categoriesDocument = View.CreateView("Categories Document", ViewCode.CategoriesDocument, 7, "/document/categories", 60, []);
        var financeDocument = View.CreateView("Finance Document", ViewCode.FinanceDocument, 8, "/document/finance", 60, []);
        var inventoryDocument = View.CreateView("Inventory Document", ViewCode.InventoryDocument, 9, "/document/inventory", 60, []);

        
        var document = View.CreateView("Document", ViewCode.Document, 6, "/document", 0,
        [
            categoriesDocument,
            financeDocument,
            inventoryDocument
        ]);

        // View for report 
        var meetingPaperReport = View.CreateView("Meeting paper report", ViewCode.MeetingPaperReport, 12, "/report/meeting/paper", 55, []);
        var meetingGraphReport = View.CreateView("Meeting graph report", ViewCode.MeetingGraphReport, 13, "/report/meeting/graph", 55, []);
        var meetingMailReport = View.CreateView("Meeting mail report", ViewCode.MeetingMailReport, 14, "/report/meeting/mail", 55, []);
        
        var meetingReport = View.CreateView("Meeting Report", ViewCode.MeetingReport, 11, "/report/meeting", 0,
        [
            meetingPaperReport,
            meetingGraphReport,
            meetingMailReport
        ]);
        
        var analyticsReport = View.CreateView("Analytics Report", ViewCode.AnalyticsReport, 15, "/report/analytics", 35, []);
        var historyReport = View.CreateView("History Report", ViewCode.HistoryReport, 16, "/report/history", 35, []);
        
        var report = View.CreateView("Report", ViewCode.Report, 10, "/report", 0,
        [
            meetingReport,
            analyticsReport,
            historyReport
        ]);

        // Seed data "Role"
        var adminRole = Role.CreateRole("Administrator", "R001", "To manage system");
        var salesExecutiveRole = Role.CreateRole("Sales Executive", "R002", "Handle sales transactions and customer relationships");
        var technicalLeadRole = Role.CreateRole("Technical Lead", "R003", "Lead technical team and oversee product development");

        // Seed table "View"
        var verifyExistedView = await viewRepository.VerifyExistedViewDataAsync(CancellationToken.None);

        if (!verifyExistedView)
        {
            // Add view first and save changes
            await viewRepository.AddViewsAsync([adminDashboard, document, report], CancellationToken.None);
            await viewRepository.UnitOfWork.SaveEntitiesAsync(CancellationToken.None);

            // Add Role Permission for View and save changes secondly
            
            // Add view for role "Admin"
            meetingPaperReport.AddViewPermissionsForRole(adminRole.Id, 63);
            meetingGraphReport.AddViewPermissionsForRole(adminRole.Id, 63);
            meetingMailReport.AddViewPermissionsForRole(adminRole.Id, 63);

            analyticsReport.AddViewPermissionsForRole(adminRole.Id, 63);
            historyReport.AddViewPermissionsForRole(adminRole.Id, 63);

            userManagement.AddViewPermissionsForRole(adminRole.Id, 63);
            roleManagement.AddViewPermissionsForRole(adminRole.Id, 63);
            productManagement.AddViewPermissionsForRole(adminRole.Id, 63);
            executedEventManagement.AddViewPermissionsForRole(adminRole.Id, 63);

            categoriesDocument.AddViewPermissionsForRole(adminRole.Id, 63);
            financeDocument.AddViewPermissionsForRole(adminRole.Id, 63);
            inventoryDocument.AddViewPermissionsForRole(adminRole.Id, 63);

            // Add view for role "Sales executive"
            meetingPaperReport.AddViewPermissionsForRole(salesExecutiveRole.Id, 51);
            meetingGraphReport.AddViewPermissionsForRole(salesExecutiveRole.Id, 55);
            meetingMailReport.AddViewPermissionsForRole(salesExecutiveRole.Id, 63);

            analyticsReport.AddViewPermissionsForRole(salesExecutiveRole.Id, 21);
            historyReport.AddViewPermissionsForRole(salesExecutiveRole.Id, 1);

            categoriesDocument.AddViewPermissionsForRole(salesExecutiveRole.Id, 3);
            financeDocument.AddViewPermissionsForRole(salesExecutiveRole.Id, 7);
            inventoryDocument.AddViewPermissionsForRole(salesExecutiveRole.Id, 3);

            userManagement.AddViewPermissionsForRole(salesExecutiveRole.Id, 51);
            roleManagement.AddViewPermissionsForRole(salesExecutiveRole.Id, 15);
            productManagement.AddViewPermissionsForRole(salesExecutiveRole.Id, 31);
            executedEventManagement.AddViewPermissionsForRole(salesExecutiveRole.Id, 63);

            // Add view for role "Technical lead"
            meetingPaperReport.AddViewPermissionsForRole(technicalLeadRole.Id, 51);
            meetingGraphReport.AddViewPermissionsForRole(technicalLeadRole.Id, 3);
            executedEventManagement.AddViewPermissionsForRole(technicalLeadRole.Id, 7);

            analyticsReport.AddViewPermissionsForRole(technicalLeadRole.Id, 3);
            historyReport.AddViewPermissionsForRole(technicalLeadRole.Id, 21);

            categoriesDocument.AddViewPermissionsForRole(technicalLeadRole.Id, 7);
            financeDocument.AddViewPermissionsForRole(technicalLeadRole.Id, 3);
            inventoryDocument.AddViewPermissionsForRole(technicalLeadRole.Id, 21);

            userManagement.AddViewPermissionsForRole(technicalLeadRole.Id, 1);
            roleManagement.AddViewPermissionsForRole(technicalLeadRole.Id, 63);
            productManagement.AddViewPermissionsForRole(technicalLeadRole.Id, 55);
            executedEventManagement.AddViewPermissionsForRole(technicalLeadRole.Id, 51);
            
            await viewRepository.UnitOfWork.SaveEntitiesAsync(CancellationToken.None);
        }

        // Seed table "Role"
        var verifyExistedRole = await roleRepository.VerifyExistedRoleDataAsync(CancellationToken.None);

        if (!verifyExistedRole)
        {
            await roleRepository.AddRolesAsync([adminRole, salesExecutiveRole, technicalLeadRole], CancellationToken.None);
            await roleRepository.UnitOfWork.SaveEntitiesAsync(CancellationToken.None);
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