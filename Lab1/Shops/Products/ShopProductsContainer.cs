using Shops.Entities;

namespace Shops.Products;
public class ShopProductsContainer : ProductsContainer
{
    public ShopProductsContainer(Shop shop)
    {
        Shop = shop;
    }

    public Shop Shop { get; private set; }

    public void AddProduct(Product product, decimal price, int amount)
    {
        if (FindProduct(product) != null)
            throw new Exception();
        Products.Add(new FullProduct(product, amount, price, Shop));
    }

    public void ReplenishProducts(Product product, int amount)
    {
        FullProduct? products = FindProduct(product);
        if (products == null)
            throw new Exception();
        products.Amount += amount;
    }
}