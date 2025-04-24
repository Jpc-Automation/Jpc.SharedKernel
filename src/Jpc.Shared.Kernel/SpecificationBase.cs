using Ardalis.Specification;

namespace Jpc.Shared.Kernel;

public abstract class SpecificationBase<TResult> : Specification<TResult>
{
    protected virtual void ApplyFilter(string? query, string? columns)
    {
    }

    protected virtual void ApplySorting(string? columns, bool descending = false)
    {

    }

    protected virtual void ApplyPaging(int skip, int take)
    {
        Query.Skip(skip).Take(take);
    }

    protected virtual void ApplyPaging1(int page, int pageSize)
    {
        Query.Skip(page * pageSize).Take(pageSize);
    }
}
