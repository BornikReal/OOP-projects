namespace Shops.Exception.ShopManagerException;

public abstract class ShopManagerException : ShopException
{
    public ShopManagerException(string message)
        : base(message)
    { }
}
