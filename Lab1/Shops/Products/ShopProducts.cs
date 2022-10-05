using Shops.Entities;

namespace Shops.Products;

public class ShopProducts
{
    private decimal? _singlePrice;
    private Shop? _shop;
    private int? _amount;
    private Product? _product;

    public Product Product
    {
        get => _product!;
        set
        {
            if (_product == null)
                _product = value;
            else
                throw new Exception();
        }
    }

    public int Amount
    {
        get => (int)_amount!;
        set
        {
            if (_amount == null && value >= 0)
                _amount = value;
            else
                throw new Exception();
        }
    }

    public decimal SinglePrice
    {
        get => (decimal)_singlePrice!;
        set
        {
            if (_singlePrice == null && value >= 0)
                _singlePrice = value;
            else
                throw new Exception();
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

    public void AddProducts(int amount)
    {
        if (amount <= 0)
            throw new Exception();
        _amount += amount;
    }

    public void RemoveProducts(int amount)
    {
        if (amount <= 0 || _amount - amount < 0)
            throw new Exception();
        _amount -= amount;
    }

    public decimal GetPrice(int amount = -1)
    {
        if (amount < 0)
            return SinglePrice * Amount / 100;
        return SinglePrice * amount / 100;
    }

    public bool Equals(ShopProducts obj)
    {
        return Product == obj.Product && Amount == obj.Amount && SinglePrice == obj.SinglePrice && Shop == obj.Shop;
    }
}