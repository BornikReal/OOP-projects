using Shops.Exception.CashAccountException;
using Shops.Products.ConcreteProduct;

namespace Shops.Models;

public class CashAccount
{
    private decimal _wallet;
    public CashAccount(decimal wallet)
    {
        Wallet = wallet;
    }

    public decimal Wallet
    {
        get => _wallet;
        set
        {
            if (value < 0)
                throw new InvalidWalletValueException(value);
            _wallet = value;
        }
    }

    public void ProcessPucrchase(ShopProduct products, int amount)
    {
        Wallet -= products.GetPrice(amount);
    }
}
