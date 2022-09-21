using Shops.Models;
using System.Diagnostics;

namespace Shops.Products;
public class Product
{
    private readonly GeneratorId _generatorId;
    public Product(string name, int id = 0)
    {
        Name = name;
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

    public bool Equals(Product obj)
    {
        return Id == obj.Id && Name == obj.Name;
    }
}