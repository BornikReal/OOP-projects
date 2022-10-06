using Shops.Entities;
using Shops.Exception.ProductsContainerException;
using Shops.Products.ConcreteProduct;

namespace Shops.Products.ProductsContainers;
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
            throw new ProductAlreadyExist(product);
        Products.Add(new FullProduct(product, amount, price, Shop));
    }
}