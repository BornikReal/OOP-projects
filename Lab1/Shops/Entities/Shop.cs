using Shops.Products;

namespace Shops.Entities;

public class Shop
{
    private ShopProductsContainer? _productsContainer;
    private string? _name;
    public Shop() { }

    public ShopProductsContainer ProductsContainer
    {
        get => _productsContainer!;
        set
        {
            if (_productsContainer != null)
                throw new Exception();
            _productsContainer = value;
        }
    }

    public string Name
    {
        get => _name!;
        set => _name = value;
    }

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