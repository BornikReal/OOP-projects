using Shops.Products.ConcreteProduct;

namespace Shops.Exception.ProductsContainerException;

public class ProductAlreadyExist : ProductsContainerException
{
    public ProductAlreadyExist(Product product)
        : base($"Product with name \"{product.Name}\" and Id \"{product.Id}\" already exist!")
    { }
}
