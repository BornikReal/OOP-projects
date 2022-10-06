using Shops.Entities;
using Shops.Products;

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
            throw new Exception();
        _shops.Add(shop);
    }

    public void RegisterProduct(Product product)
    {
        if (ContainsProduct(product))
            throw new Exception();
        _products.Add(product);
    }

    public void SetNewPrice(Shop shop, Product product, decimal price)
    {
        if (price < 0)
            throw new Exception();
        if (!ContainsProduct(product))
            throw new Exception();
        if (!ContainsShop(shop))
            throw new Exception();
        FullProduct? res = shop.ProductsContainer.FindProduct(product);
        if (res == null)
            throw new Exception();
        res.SinglePrice = price;
    }

    public void BuyCheapest(Person person, Product product, int amount)
    {
        if (!ContainsProduct(product))
            throw new Exception();
        FullProduct? cur_product = null, buf_product;
        foreach (Shop shop in _shops)
        {
            buf_product = shop.ProductsContainer.FindProduct(product);
            if (buf_product == null || buf_product.Amount < amount)
                continue;
            if (cur_product == null || buf_product.GetPrice(amount) < cur_product.GetPrice(amount))
                cur_product = buf_product;
        }

        if (cur_product == null)
            throw new Exception();
        person.Wallet.Buy(cur_product.Shop!, product, amount);
    }

    public void BuyProducts(Person person, Shop shop, UserProductsContainer products)
    {
        if (!ContainsShop(shop))
            throw new Exception();
        foreach (FullProduct cort in products.UserProducts)
        {
            if (!ContainsProduct(cort.Product))
                throw new Exception();
            person.Wallet.Buy(shop, cort.Product, cort.Amount);
        }
    }
}
