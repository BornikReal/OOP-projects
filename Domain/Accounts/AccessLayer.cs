namespace Domain.Accounts;

public record class AccessLayer
{
    public AccessLayer(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Access must be upper than 0");

        Value = value;
    }

    protected AccessLayer() { }

    public int Value { get; protected init; }

    public static implicit operator int(AccessLayer accessLayer) => accessLayer.Value;
    public static implicit operator AccessLayer(int value) => new AccessLayer(value);
}
