using Shops.Entities;
using Shops.Exception.ProductException;

namespace Shops.Products.ConcreteProduct;

public class ShopProduct
{
    private decimal _singlePrice;
    private int _amount;

    public ShopProduct(Product product, int amount, decimal singlePrice, Shop shop)
    {
        Product = product;
        Amount = amount;
        _singlePrice = singlePrice;
        Shop = shop;
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

    public decimal SinglePrice
    {
        get => _singlePrice;
        set
        {
            if (value < 0)
                throw new InvalidPriceException(value);
            _singlePrice = value;
        }
    }

    public Shop Shop { get; private set; }

    public decimal GetPrice(int amount = -1)
    {
        if (amount < 0)
            return SinglePrice * Amount;
        return SinglePrice * amount;
    }
}