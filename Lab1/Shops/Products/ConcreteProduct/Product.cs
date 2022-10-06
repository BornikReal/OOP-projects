namespace Shops.Products.ConcreteProduct;
public class Product
{
    public Product(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public Guid Id { get; } = Guid.NewGuid();

    public bool Equals(Product obj)
    {
        return Id == obj.Id;
    }
}