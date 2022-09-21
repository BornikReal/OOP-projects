namespace Shops.Entities;

public class Person
{
    public Person(string name, int wallet)
    {
        Name = name;
        Wallet = wallet;
    }

    public string Name { get; set; }
    public float Wallet { get; set; }
}
