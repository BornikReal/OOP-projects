using Shops.Entities;
using Shops.Products.ConcreteProduct;

namespace Shops.Models;

public class CashAccount
{
    public CashAccount(decimal wallet)
    {
        if (wallet < 0)
            throw new Exception();
        Wallet = wallet;
    }

    public decimal Wallet { get; private set; }

    public FullProduct? Sellable { get; private set; } = null;
    public int SellAmount { get; private set; } = 0;

    public FullProduct Buy(Shop shop, Product product, int amount)
    {
        FullProduct? products = shop.ProductsContainer.FindProduct(product);
        if (products == null)
            throw new Exception();
        if (Wallet < products.GetPrice(amount))
            throw new Exception();
        Sellable = products;
        SellAmount = amount;
        shop.Buy(this);
        Wallet -= products.GetPrice(amount);
        Sellable = null;
        SellAmount = 0;
        return FullProduct.Clone(products);
    }
}
