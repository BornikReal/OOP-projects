using Shops.Entities;
using Shops.Products.ConcreteProduct;
using Shops.Products.ProductsContainers;

namespace Shops.Services;

public interface IShopManager
{
    bool ContainsShop(Shop shop);
    bool ContainsProduct(Product product);
    void RegisterShop(Shop shop);
    void RegisterProduct(Product product);
    void SetNewPrice(Shop shop, Product product, decimal price);
    Shop FindCheapest(Product product, int amount);
    void BuyProducts(Person person, Shop shop, UserProductsContainer products);
}