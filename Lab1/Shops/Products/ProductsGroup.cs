using Shops.Entities;

namespace Shops.Products;

public class ProductsGroup
{
    private float _discount;
    private Shop? _shop;

    public ProductsGroup(Product product, int amount)
    {
        Product = product;
        Amount = amount;
        Discount = 0;
    }

    public Product Product { get; }
    public int Amount { get; }
    public Shop? Shop
    {
        get => _shop;
        set
        {
            ChangeShop(value);
        }
    }

    public float Discount
    {
        get => _discount;
        set
        {
            if (value is <= 0 or >= 100)
                throw new Exception();
            _discount = value;
        }
    }

    public float GetPrice(int amount)
    {
        if (amount < 0 || amount > Amount)
            amount = Amount;
        return Product.Price * amount * (100 - Discount) / 100;
    }

    public void SetPrice(int price)
    {
        Product.Price = price;
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