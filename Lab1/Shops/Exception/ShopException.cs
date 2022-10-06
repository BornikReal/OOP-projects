namespace Shops.Exception;

public abstract class ShopException : IOException
{
    public ShopException(string message)
        : base(message)
    { }
}
