using Shops.Products.ConcreteProduct;

namespace Shops.Exception.ShopManagerException;

public class ProductAlreadyRegisteredException : ShopManagerException
{
    public ProductAlreadyRegisteredException(Product product)
        : base($"Product with name \"{product.Name}\" and Id \"{product.Id}\" already registered!")
    { }
}
