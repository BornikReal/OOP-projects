using Shops.Entities;

namespace Shops.Products;

public class ProductsGroup
{
    private decimal _discount;
    private decimal _price;
    private Shop? _shop;
    private int _amount;

    public ProductsGroup(Product product, int price, int amount)
    {
        Product = product;
        Amount = amount;
        Price = price;
        Discount = 0;
    }

    public Product Product { get; }
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

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new Exception();
            _price = value;
        }
    }

    public Shop? Shop
    {
        get => _shop;
        set
        {
            ChangeShop(value);
        }
    }

    public decimal Discount
    {
        get => _discount;
        set
        {
            if (value is <= 0 or >= 100)
                throw new Exception();
            _discount = value;
        }
    }

    public decimal GetPrice(int amount = -1)
    {
        if (amount < 0 || amount > Amount)
            amount = Amount;
        return Price * amount * (100 - Discount) / 100;
    }

    public bool Equals(ProductsGroup obj)
    {
        return Product == obj.Product && Amount == obj.Amount && Price == obj.Price && Discount == obj.Discount;
    }

    private void ChangeShop(Shop? new_shop)
    {
        if (new_shop != null && new_shop.FindProducts(this) == null)
            throw new Exception();
        else if (_shop != null && _shop.FindProducts(this) != null)
            throw new Exception();
        _shop = new_shop;
    }
}