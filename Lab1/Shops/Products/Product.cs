using Shops.Models;
namespace Shops.Products;
public class Product
{
    private readonly GeneratorId _generatorId;
    private float _price;
    public Product(string name, float price, int id = 0)
    {
        Name = name;
        Price = price;
        _generatorId = new GeneratorId("ProductsId.json");
        if (id == 0)
        {
            Id = _generatorId.Generate();
        }
        else if (id >= _generatorId.MinId && id <= _generatorId.MaxId)
        {
            _generatorId.UseID(id);
            Id = id;
        }
        else
        {
            throw new Exception();
        }
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