using Shops.Entities;
using Shops.Products;

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

    public ShopProduct? Sellable { get; private set; } = null;
    public int SellAmount { get; private set; } = 0;

    public ShopProduct Buy(Shop shop, Product product, int amount)
    {
        ShopProduct? products = shop.ProductsContainer.FindProduct(product);
        if (products == null)
            throw new Exception();
        if (Wallet < products.GetPrice(amount))
            throw new Exception();
        Sellable = products;
        SellAmount = amount;
        shop.Buy(this);
        Sellable = null;
        SellAmount = 0;
        return ShopProduct.Clone(products);
    }
}
