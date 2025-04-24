namespace Jpc.Shared.Kernel;

/// <summary>
/// A simple interface for sending domain events. Can use MediatR or any other implementation.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<HasDomainEventsBase> entitiesWithEvents);
}
