using Shops.Entities;
using Shops.Products;

namespace Shops.Services;

public interface IShopManager
{
    public Shop CreateShop(string name, string address);
    public Person RegisterPerson(Person person);
    public Product RegisterProduct(Product product);
    public ProductsGroup AddProductsToShop(Product product, decimal price, int amount);
    public void BuyCheapest(Person person, Product product, int amount);
    public void BuyProducts(Person person, List<(Product, int)> products);
}