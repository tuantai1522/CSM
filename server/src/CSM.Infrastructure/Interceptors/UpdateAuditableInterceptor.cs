using CSM.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CSM.Infrastructure.Interceptors;

/// <summary>
/// This interceptor will update auditable value if entities implement interface IAuditableEntity
/// </summary>
internal sealed class UpdateAuditableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableEntities(DbContext context)
    {
        long milliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var entities = context.ChangeTracker.Entries<IAuditableEntity>().ToList();

        foreach (EntityEntry<IAuditableEntity> entry in entities)
        {
            if (entry.State == EntityState.Added)
            {
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.CreatedAt), milliseconds);
            }
            else if (entry.State == EntityState.Modified)
            {
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.UpdatedAt), milliseconds);
            }
            else if (entry.State == EntityState.Deleted)
            {
                SetCurrentPropertyValue(entry, nameof(IAuditableEntity.DeletedAt), milliseconds);
            }
        }

        static void SetCurrentPropertyValue(EntityEntry entry, string propertyName, long milliseconds) 
            => entry.Property(propertyName).CurrentValue = milliseconds;
    }
}