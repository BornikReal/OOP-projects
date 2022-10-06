namespace Shops.Exception.ProductException;

internal class InvalidProductAmount : ShopException
{
    public InvalidProductAmount(int amount)
        : base($"Invalid product amount: {amount}!\nProduct amount must be not negative number.")
    { }
}
