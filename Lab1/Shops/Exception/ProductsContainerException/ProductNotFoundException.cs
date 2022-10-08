using Shops.Products.ConcreteProduct;

namespace Shops.Exception.ProductsContainerException;

public class ProductNotFoundException : ProductsContainerException
{
    public ProductNotFoundException(Product product)
        : base($"Product with name \"{product.Name}\" and Id \"{product.Id}\" not found!")
    { }
}
