using MediatR;

namespace Jpc.Shared.Kernel;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, IEnumerable<HasDomainEventsBase> entitiesWithEvents)
    {
        var domainEntities = entitiesWithEvents
            .Where(x => x.DomainEvents != null && x.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
