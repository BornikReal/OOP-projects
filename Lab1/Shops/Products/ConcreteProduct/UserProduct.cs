using Shops.Exception.ProductException;

namespace Shops.Products.ConcreteProduct;

public class UserProduct
{
    private int _amount;

    public UserProduct(Product product, int amount)
    {
        Product = product;
        Amount = amount;
    }

    public Product Product { get; private set; }

    public int Amount
    {
        get => _amount;
        set
        {
            if (value < 0)
                throw new InvalidProductAmount(value);
            _amount = value;
        }
    }
}