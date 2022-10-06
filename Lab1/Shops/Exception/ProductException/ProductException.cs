namespace Shops.Exception.ProductException;

public abstract class ProductException : ShopException
{
    public ProductException(string message)
        : base(message)
    { }
}
