using Shops.Exception.ProductsContainerException;
using Shops.Products.ConcreteProduct;

namespace Shops.Products.ProductsContainers;

public class UserProductsContainer
{
    public List<UserProduct> UserProducts
    {
        get => Products;
    }

    private List<UserProduct> Products { get; } = new List<UserProduct>();
    public UserProduct? FindProduct(Product product)
    {
        return Products.Find(s => s.Product == product);
    }

    public void RemoveProduct(Product product)
    {
        UserProduct? removable = FindProduct(product);
        if (removable == null)
            throw new ProductNotFoundException(product);
        Products.Remove(removable);
    }

    public void AddProduct(Product product, int amount)
    {
        if (FindProduct(product) != null)
            throw new ProductAlreadyExist(product);
        Products.Add(new UserProduct(product, amount));
    }
}