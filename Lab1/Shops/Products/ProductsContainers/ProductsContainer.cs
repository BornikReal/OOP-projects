using Shops.Products.ConcreteProduct;

namespace Shops.Products.ProductsContainers;

public abstract class ProductsContainer
{
    protected List<FullProduct> Products { get; } = new List<FullProduct>();

    public FullProduct? FindProduct(Product product)
    {
        return Products.Find(s => s.Product == product);
    }

    public void RemoveProduct(Product product)
    {
        FullProduct? removable = FindProduct(product);
        if (removable == null)
            throw new Exception();
        Products.Remove(removable);
    }
}
