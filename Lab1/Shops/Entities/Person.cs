using Shops.Models;

namespace Shops.Entities;

public class Person
{
    public Person(string name, CashAccount account)
    {
        Name = name;
        Wallet = account;
    }

    public string Name { get; set; }

    public CashAccount Wallet { get; }
}