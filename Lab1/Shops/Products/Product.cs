using Shops.Models;
namespace Shops.Products;
public class Product
{
    private readonly GeneratorId _generatorId;
    private float _price;
    public Product(string name, float price)
    {
        Name = name;
        Price = price;
        _generatorId = new GeneratorId("ProductsId.json");
        Id = _generatorId.Generate();
    }

    public string Name { get; set; }
    public int Id { get; }
    public float Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new Exception();
            _price = value;
        }
    }
}