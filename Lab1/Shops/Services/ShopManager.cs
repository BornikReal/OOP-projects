using Shops.Entities;
using Shops.Exception.ProductsContainerException;
using Shops.Exception.ShopManagerException;
using Shops.Products.ConcreteProduct;
using Shops.Products.ProductsContainers;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops = new List<Shop>();
    private readonly List<Product> _products = new List<Product>();

    public ShopManager() { }
    public IReadOnlyList<Shop> Shops => _shops!;
    public IReadOnlyList<Product> Products => _products!;

    public bool ContainsShop(Shop shop)
    {
        return _shops.Find(s => s == shop) != null;
    }

    public bool ContainsProduct(Product product)
    {
        return _products.Find(s => s == product) != null;
    }

    public void RegisterShop(Shop shop)
    {
        if (ContainsShop(shop))
            throw new ShopAlreadyRegisteredException(shop);
        _shops.Add(shop);
    }

    public void RegisterProduct(Product product)
    {
        if (ContainsProduct(product))
            throw new ProductAlreadyRegisteredException(product);
        _products.Add(product);
    }

    public void SetNewPrice(Shop shop, Product product, decimal price)
    {
        if (!ContainsProduct(product))
            throw new ProductDoesNotRegisteredException(product);
        if (!ContainsShop(shop))
            throw new ShopDoesNotRegisteredException(shop);
        ShopProduct? res = shop.ProductsContainer.FindProduct(product);
        if (res == null)
            throw new ProductNotFoundException(product);
        res.SinglePrice = price;
    }

    public Shop FindCheapest(Product product, int amount)
    {
        if (!ContainsProduct(product))
            throw new ProductDoesNotRegisteredException(product);
        ShopProduct? cur_product = null, buf_product;
        foreach (Shop shop in _shops)
        {
            buf_product = shop.ProductsContainer.FindProduct(product);
            if (buf_product == null || buf_product.Amount < amount)
                continue;
            if (cur_product == null || buf_product.GetPrice(amount) < cur_product.GetPrice(amount))
                cur_product = buf_product;
        }

        if (cur_product == null)
            throw new ProductNotFoundException(product);
        return cur_product.Shop;
    }

    public void BuyProducts(Person person, Shop shop, UserProductsContainer products)
    {
        if (!ContainsShop(shop))
            throw new ShopDoesNotRegisteredException(shop);
        foreach (UserProduct cort in products.UserProducts)
        {
            if (!ContainsProduct(cort.Product))
                throw new ProductDoesNotRegisteredException(cort.Product);
            shop.Buy(person, cort.Product, cort.Amount);
        }
    }
}
