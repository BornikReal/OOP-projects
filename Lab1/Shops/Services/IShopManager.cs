using Shops.Entities;
using Shops.Products.ConcreteProduct;
using Shops.Products.ProductsContainers;

namespace Shops.Services;

public interface IShopManager
{
    public bool ContainsShop(Shop shop);
    public bool ContainsProduct(Product product);
    public void RegisterShop(Shop shop);
    public void RegisterProduct(Product product);
    public void SetNewPrice(Shop shop, Product product, decimal price);
    public void BuyCheapest(Person person, Product product, int amount);
    public void BuyProducts(Person person, Shop shop, UserProductsContainer products);
}