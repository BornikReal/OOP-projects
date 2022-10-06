namespace Shops.Exception.ProductsContainerException;

public abstract class ProductsContainerException : ShopException
{
    public ProductsContainerException(string message)
        : base(message)
    { }
}