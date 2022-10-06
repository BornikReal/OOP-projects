using Shops.Models;

namespace Shops.Entities;

public class Person
{
    private string? _name;
    private CashAccount? _account;
    public Person() { }

    public string Name
    {
        get => _name!;
        set => _name = value;
    }

    public CashAccount Wallet
    {
        get => _account!;
        set
        {
            if (_account != null)
                throw new Exception();
            _account = value;
        }
    }
}