namespace CSM.Core.Features.Views;

public enum ViewCode
{
    /// <summary>
    /// Admin dashboard
    /// </summary>
    Dashboard = 0,
    UserManagement = 1,
    RoleManagement = 2,
    ProductManagement = 3,
    ExecutedEventManagement = 4,
    
    /// <summary>
    /// Document
    /// </summary>
    Document = 5,
    CategoriesDocument = 6,
    FinanceDocument = 7,
    InventoryDocument = 8,
    
    /// <summary>
    /// Report
    /// </summary>
    Report = 9,
    MeetingReport = 10,
    AnalyticsReport = 11,
    HistoryReport = 12,
    
    MeetingPaperReport = 13,
    MeetingGraphReport = 14,
    MeetingMailReport = 15,
}