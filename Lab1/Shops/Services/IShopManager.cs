using Shops.Entities;
using Shops.Products;

namespace Shops.Services;

public interface IShopManager
{
    public Shop CreateShop(string name, string address);
    public void RegisterProduct(Product product);
    public ProductsGroup AddProductsToShop(Shop shop, Product product, decimal price, int amount);
    public void BuyCheapest(Person person, Product product, int amount);
    public void BuyProducts(Person person, Shop shop, List<(Product, int)> products);
}