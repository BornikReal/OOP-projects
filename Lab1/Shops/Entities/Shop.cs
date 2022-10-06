using Shops.Models;
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

    public void Buy(CashAccount acc)
    {
        if (acc.Sellable == null)
            throw new Exception();
        if (acc.Sellable.Shop != this)
            throw new Exception();
        acc.Sellable.RemoveProducts(acc.SellAmount);
    }

    public bool Equals(Shop obj)
    {
        return Id == obj.Id;
    }
}