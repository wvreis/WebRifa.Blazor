namespace WebRifa.Blazor.Core.Common;
public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public Guid? CreatedBy { get; private set; }

    public void SetCreatedBy(Guid userId)
    {
        CreatedBy = userId;
    }

    public void SetUpdatedAt() {
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDeleted() { 
        IsDeleted = true; 
    }
}