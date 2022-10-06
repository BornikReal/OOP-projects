using Shops.Models;

namespace Shops.Entities;

public class Person
{
    public Person(string name, CashAccount wallet)
    {
        Name = name;
        Wallet = wallet;
    }

    public string Name { get; set; }

    public CashAccount Wallet { get; private set; }
}