namespace Domain.Accounts;

public record class AccessLayer
{
    private int _access;
    public int Access
    {
        get => _access;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Access must be upper than 0");
                
            _access = value;
        }
    }

    public AccessLayer(int access) => Access = access;
}
