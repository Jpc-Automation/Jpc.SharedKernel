using System.ComponentModel.DataAnnotations.Schema;

namespace Jpc.SharedKernel;

public abstract class HasDomainEventsBase
{
    private List<DomainEventBase> _domainEvents = new();
    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected virtual void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    internal virtual void ClearDomainEvents() => _domainEvents.Clear();
}

