using Shops.Models;
using Shops.Products;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ProductsGroup> _products;
    private readonly GeneratorId _generatorId;
    public Shop(string name, string address)
    {
        Name = name;
        Adress = address;
        _products = new List<ProductsGroup>();
        _generatorId = new GeneratorId("ShopsId.json");
        Id = _generatorId.Generate();
    }

    public IReadOnlyList<ProductsGroup> Products => _products;
    public string Name { get; set; }
    public string Adress { get; set; }
    public int Id { get; }
    public void RemoveProducts(ProductsGroup new_products)
    {
        if (new_products.Shop == this)
        {
            _products.Remove(new_products);
            new_products.Shop = null;
        }
        else
        {
            throw new Exception();
        }
    }

    public void AddProducts(ProductsGroup new_products)
    {
        if (new_products.Shop == null)
        {
            _products.Add(new_products);
            new_products.Shop = this;
        }
        else
        {
            throw new Exception();
        }
    }

    public ProductsGroup? FindProducts(ProductsGroup? products)
    {
        return _products.Find(s => s == products);
    }
}