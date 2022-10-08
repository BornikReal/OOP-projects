using Shops.Exception.ProductException;
using Shops.Exception.ProductsContainerException;
using Shops.Products.ConcreteProduct;
using Shops.Products.ProductsContainers;

namespace Shops.Entities;

public class Shop
{
    public Shop(string name)
    {
        Name = name;
        ProductsContainer = new ShopProductsContainer(this);
    }

    public ShopProductsContainer ProductsContainer { get; }

    public string Name { get; set; }

    public Guid Id { get; } = Guid.NewGuid();

    public void Buy(Person person, Product product, int amount)
    {
        FullProduct? fullProduct = ProductsContainer.FindProduct(product);
        if (fullProduct == null)
            throw new ProductNotFoundException(product);
        if (fullProduct.Amount < amount)
            throw new InvalidProductAmount(amount);
        person.Wallet.ProcessPucrchase(fullProduct, amount);
        fullProduct.Amount -= amount;
    }

    public bool Equals(Shop obj)
    {
        return Id == obj.Id;
    }
}