namespace Shops.Exception.CashAccountException;

public abstract class CashAccountException : ShopException
{
    public CashAccountException(string message)
        : base(message)
    { }
}
