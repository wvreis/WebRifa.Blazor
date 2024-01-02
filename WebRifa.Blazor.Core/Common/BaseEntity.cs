﻿namespace WebRifa.Blazor.Core.Common;
public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }
    public bool IdDeleted { get; private set; }

    public void Update() {
        UpdatedAt = DateTime.UtcNow;
    }
}