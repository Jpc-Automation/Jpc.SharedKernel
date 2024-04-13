namespace Jpc.SharedKernel;

public abstract class AggregateRoot<TId> : EntityBase<TId> where TId : struct, IEquatable<TId>
{
    protected override void RegisterDomainEvent(DomainEventBase domainEvent)
    {
        if ((object)Id is not null)
            domainEvent.SetAggregateId(Id.ToString() ?? "");

        base.RegisterDomainEvent(domainEvent);
    }
}
