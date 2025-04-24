namespace Jpc.Shared.Kernel;

public abstract class AggregateRoot<TId> : EntityBase<TId> where TId : IEquatable<TId>
{
    protected override void RegisterDomainEvent(DomainEventBase domainEvent)
    {
        if (Id is not null)
            domainEvent.SetAggregateId(Id.ToString() ?? string.Empty);

        base.RegisterDomainEvent(domainEvent);
    }
}
