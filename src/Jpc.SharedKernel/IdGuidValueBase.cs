using System.ComponentModel;

namespace Jpc.SharedKernel;

public abstract class Id<T> : ValueObject where T : struct
{
    public T Value { get; }

    protected Id(T value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException($"Invalid ID value '{value}'.");
        }

        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }


    // Static factory methods for creating instances of derived classes
    public static Id<T> Create(T value) => IsGuidType(typeof(T)) ? new GuidId(Guid.NewGuid()) as Id<T> : new GenericId<T>(value);

    // Parse method for converting a string representation of the ID to its actual value
    public static Id<T> Parse(string id)
    {
        try
        {
            if (IsGuidType(typeof(T)))
            {
                return new GuidId(Guid.Parse(id)) as Id<T>;
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return new GenericId<T>((T)converter.ConvertFromInvariantString(id));
            }
        }
        catch (FormatException e)
        {
            throw new FormatException("Could not parse ID value from given string.", e);
        }
        catch (OverflowException e)
        {
            throw new OverflowException("Given string represents a number outside the range of the destination type.", e);
        }
    }


    // Helper method to check if T is a Guid type
    private static bool IsGuidType(Type t) => t == (Nullable.GetUnderlyingType(t) ?? typeof(Guid));

    // Helper method to validate the ID value
    private static bool IsValid(T value) => !default(T).Equals(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

// Derived class for handling Guids specifically
public sealed class GuidId : Id<Guid>
{
    public GuidId(Guid value) : base(value) { }
}

// Derived class for handling non-Guid types generically
public sealed class GenericId<T> : Id<T> where T : struct
{
    public GenericId(T value) : base(value) { }
}