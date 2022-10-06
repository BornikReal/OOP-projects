using Shops.Entities;
using Shops.Exception.ProductException;

namespace Shops.Products.ConcreteProduct;

public class FullProduct
{
    private decimal? _singlePrice;
    private int _amount;

    public FullProduct(Product product, int amount)
    {
        Product = product;
        Amount = amount;
    }

    public FullProduct(Product product, int amount, decimal price, Shop shop)
    {
        Product = product;
        Amount = amount;
        SinglePrice = price;
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
        get => (decimal)_singlePrice!;
        set
        {
            if (value < 0)
                throw new InvalidPriceException(value);
            _singlePrice = value;
        }
    }

    public Shop? Shop { get; private set; }

    public static FullProduct Clone(FullProduct clonable)
    {
        var obj = new FullProduct(new Product(clonable.Product.Name), clonable.Amount)
        {
            SinglePrice = clonable.SinglePrice,
            Shop = clonable.Shop,
        };
        return obj;
    }

    public decimal GetPrice(int amount = -1)
    {
        if (amount < 0)
            return SinglePrice * Amount;
        return SinglePrice * amount;
    }

    public bool Equals(FullProduct obj)
    {
        return Product == obj.Product && Amount == obj.Amount && SinglePrice == obj.SinglePrice && Shop == obj.Shop;
    }
}