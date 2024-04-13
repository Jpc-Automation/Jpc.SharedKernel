using MediatR;

namespace Jpc.SharedKernel;

/// <summary>
/// A base type for domain events. Depends on MediatR INotification.
/// Includes DateOccurred which is set on creation.
/// </summary>
public abstract class DomainEventBase : INotification
{
    public string AggregateId { get; protected set; } = string.Empty;
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;

    internal void SetAggregateId(string aggregateId)
    {
        if (string.IsNullOrEmpty(AggregateId))
            AggregateId = aggregateId;
    }
}

