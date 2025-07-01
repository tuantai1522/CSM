namespace CSM.Core.Common;

public interface IAuditableEntity
{
    public long CreatedAt { get; }
    
    public long? UpdatedAt { get; }
    
    public long? DeletedAt { get; }
}