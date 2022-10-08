namespace Shops.Exception.ProductException;

public class InvalidPriceException : ProductException
{
    public InvalidPriceException(decimal price)
        : base($"Invalid product price: {price}!\nProduct price must be not negative number.")
    { }
}
