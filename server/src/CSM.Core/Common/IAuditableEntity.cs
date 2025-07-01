namespace CSM.Core.Common;

public interface IAuditableEntity
{
    public long CreatedAt { get; set; }
    
    public long? UpdatedAt { get; set; }
    
    public long? DeletedAt { get; set; }
}