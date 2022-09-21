namespace Shops.Entities;

public class Person
{
    private decimal _wallet;

    public Person(string name, int wallet)
    {
        Name = name;
        Wallet = wallet;
    }

    public string Name { get; set; }
    public decimal Wallet
    {
        get => _wallet;
        set
        {
            if (value < 0)
                throw new Exception();
            _wallet = value;
        }
    }
}