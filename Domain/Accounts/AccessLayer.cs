namespace Domain.Accounts;

public readonly record struct AccessLayer
{
    public AccessLayer(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Access must be upper than 0");

        Value = value;
    }

    public int Value { get; }

    public static implicit operator int(AccessLayer accessLayer) => accessLayer.Value;
    public static implicit operator AccessLayer(int value) => new AccessLayer(value);
}
