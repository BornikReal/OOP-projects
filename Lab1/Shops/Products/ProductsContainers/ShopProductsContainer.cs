using Shops.Entities;
using Shops.Exception.ProductsContainerException;
using Shops.Products.ConcreteProduct;

namespace Shops.Products.ProductsContainers;
public class ShopProductsContainer
{
    public ShopProductsContainer(Shop shop)
    {
        Shop = shop;
    }

    public Shop Shop { get; private set; }
    private List<ShopProduct> Products { get; } = new List<ShopProduct>();

    public ShopProduct? FindProduct(Product product)
    {
        return Products.Find(s => s.Product == product);
    }

    public void RemoveProduct(Product product)
    {
        ShopProduct? removable = FindProduct(product);
        if (removable == null)
            throw new ProductNotFoundException(product);
        Products.Remove(removable);
    }

    public void AddProduct(Product product, decimal price, int amount)
    {
        if (FindProduct(product) != null)
            throw new ProductAlreadyExist(product);
        Products.Add(new ShopProduct(product, amount, price, Shop));
    }
}