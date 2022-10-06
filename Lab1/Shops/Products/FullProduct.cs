using Shops.Entities;

namespace Shops.Products;

public class FullProduct
{
    private decimal? _singlePrice;
    private Shop? _shop;
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
                throw new Exception();
            _amount = value;
        }
    }

    public decimal SinglePrice
    {
        get => (decimal)_singlePrice!;
        set
        {
            if (value < 0)
                throw new Exception();
            _singlePrice = value;
        }
    }

    public Shop? Shop
    {
        get => _shop;
        set
        {
            if (_shop == null)
                _shop = value;
            else
                throw new Exception();
        }
    }

    public static FullProduct Clone(FullProduct clonable)
    {
        var obj = new FullProduct(new Product(clonable.Product.Name), clonable.Amount)
        {
            _singlePrice = clonable.SinglePrice,
            _shop = clonable.Shop,
        };
        return obj;
    }

    public decimal GetPrice(int amount = -1)
    {
        if (amount < 0)
            return SinglePrice * Amount / 100;
        return SinglePrice * amount / 100;
    }

    public bool Equals(FullProduct obj)
    {
        return Product == obj.Product && Amount == obj.Amount && SinglePrice == obj.SinglePrice && Shop == obj.Shop;
    }
}