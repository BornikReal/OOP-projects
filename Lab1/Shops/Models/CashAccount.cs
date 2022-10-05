namespace Shops.Models;

public class CashAccount
{
    public CashAccount(decimal wallet)
    {
        Wallet = wallet;
    }

    public decimal Wallet { get; }
}
