using Shops.Entities;
using Shops.Products;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops;
    private readonly List<Person> _pesons;
    private readonly List<Product> _products;

    public ShopManager()
    {
        _shops = new List<Shop>();
        _pesons = new List<Person>();
        _products = new List<Product>();
    }

    public IReadOnlyList<Shop> Shops => _shops;
    public IReadOnlyList<Person> Persons => _pesons;
    public IReadOnlyList<Product> Products => _products;

    public Shop CreateShop(string name, string address)
    {
        var new_shop = new Shop(name, address);
        if (_shops.Find(s => s == new_shop) != null)
            throw new Exception();
        _shops.Add(new_shop);
        return new_shop;
    }

    public void RegisterProduct(Product product)
    {
        if (_products.Find(s => s == product) != null)
            throw new Exception();
        _products.Add(product);
    }

    public ProductsGroup AddProductsToShop(Shop shop, Product product, decimal price, int amount)
    {
        if (_shops.Find(s => s == shop) == null)
            throw new Exception();
        if (_products.Find(s => s == product) == null)
            throw new Exception();
        var new_products = new ProductsGroup(product, price, amount);
        shop.AddProducts(new_products);
        return new_products;
    }

    public void BuyCheapest(Person person, Product product, int amount)
    {
        ProductsGroup? cur_product = null, buf_product;
        foreach (Shop shop in _shops)
        {
            buf_product = shop.GetProductsGroup(product);
            if (buf_product == null || buf_product.Amount < amount)
                continue;
            if (cur_product == null || buf_product.GetPrice(amount) < cur_product.GetPrice(amount))
                cur_product = buf_product;
        }

        if (cur_product == null)
            throw new Exception();
        cur_product.Shop!.Buy(person, cur_product, amount);
    }

    public void BuyProducts(Person person, Shop shop, List<(Product, int)> products)
    {
        ProductsGroup? cur_product;
        foreach ((Product, int) cort in products)
        {
            cur_product = shop.GetProductsGroup(cort.Item1);
            if (cur_product == null)
                throw new Exception();
            shop.Buy(person, cur_product, cort.Item2);
        }
    }
}
