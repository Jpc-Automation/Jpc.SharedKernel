
namespace Jpc.SharedKernel;

/// <summary>
/// A simple interface for sending domain events. Can use MediatR or any other implementation.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<HasDomainEventsBase> entitiesWithEvents);
    Task DispatchAndClearEvents<TId>(IEnumerable<EntityBase<TId>> entitiesWithEvents) where TId : struct, IEquatable<TId>;
}
