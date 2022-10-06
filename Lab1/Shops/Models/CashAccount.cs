using Shops.Entities;
using Shops.Exception.CashAccountException;
using Shops.Exception.ProductsContainerException;
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

    public FullProduct? Sellable { get; private set; } = null;
    public int SellAmount { get; private set; } = 0;

    public FullProduct Buy(Shop shop, Product product, int amount)
    {
        FullProduct? products = shop.ProductsContainer.FindProduct(product);
        if (products == null)
            throw new ProductNotFoundException(product);
        Sellable = products;
        SellAmount = amount;
        shop.Buy(this);
        Wallet -= products.GetPrice(amount);
        Sellable = null;
        SellAmount = 0;
        return FullProduct.Clone(products);
    }
}
