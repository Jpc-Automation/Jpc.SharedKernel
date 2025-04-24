namespace Jpc.Shared.Kernel.UnitTests.ValueObjectTests;

public class TestValueObject : ValueObject
{
    public int Value { get; }

    public TestValueObject(int value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
