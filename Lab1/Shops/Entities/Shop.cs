using Shops.Products;

namespace Shops.Entities;

public class Shop
{
    public Shop(string name)
    {
        Name = name;
    }

    public ShopProductsContainer ProductsContainer { get; } = new ShopProductsContainer();
    public string Name { get; set; }
    public Guid Id { get; } = Guid.NewGuid();

    // TODO
    public void Buy(Person person, Product product, int amount)
    {
        ShopProduct? products = ProductsContainer.FindProduct(product);
        if (products == null)
            throw new Exception();
        if (person.Wallet < products.GetPrice(amount))
            throw new Exception();
        products.RemoveProducts(amount);
    }

    public bool Equals(Shop obj)
    {
        return Id == obj.Id;
    }
}