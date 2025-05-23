﻿namespace Jpc.Shared.Kernel;

/// <summary>
/// A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
/// If you prefer GUID Ids, change it here.
/// If you need to support both GUID and int IDs, change to EntityBase&lt;TId&gt; and use TId as the type for Id.
/// </summary>
public abstract class EntityBase : HasDomainEventsBase
{
    public Guid Id { get; protected set; }
}

//public abstract class EntityBase<TId> : HasDomainEventsBase
//  where TId : struct, IEquatable<TId>
//{
//    public TId Id { get; set; }
//}

public abstract class EntityBase<TId> : HasDomainEventsBase
    where TId : IEquatable<TId>
{
    public TId Id { get; protected set; } = default!;
}
