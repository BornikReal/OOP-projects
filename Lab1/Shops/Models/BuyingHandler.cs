using Shops.Entities;
using Shops.Products;

namespace Shops.Models;

public class BuyingHandler
{
    private readonly Shop _shop;

    public BuyingHandler(Shop shop)
    {
        _shop = shop;
    }

    public void Buy(Person person, Product product, int amount)
    {
        ShopProducts? products = _shop.FindProduct(product);
        if (products == null)
            throw new Exception();
        if (person.Wallet < products.GetPrice(amount))
            throw new Exception();
        products.RemoveProducts(amount);
    }
}
