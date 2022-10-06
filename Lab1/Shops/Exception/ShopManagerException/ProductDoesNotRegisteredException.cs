using Shops.Products.ConcreteProduct;

namespace Shops.Exception.ShopManagerException;

public class ProductDoesNotRegisteredException : ShopManagerException
{
    public ProductDoesNotRegisteredException(Product product)
        : base($"Product with name \"{product.Name}\" and Id \"{product.Id}\" does not registered!")
    { }
}
