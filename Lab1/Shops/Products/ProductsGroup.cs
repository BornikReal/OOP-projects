namespace Shops.Products;

public class ProductsGroup
{
    private float _discount;

    public ProductsGroup(Product product, int amount)
    {
        Product = product;
        Amount = amount;
        Discount = 0;
    }

    public Product Product { get; }
    public int Amount { get; }
    public int ShopID { get; }
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
}
