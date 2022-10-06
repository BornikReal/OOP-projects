namespace Shops.Products;

public class UserProductsContainer : ProductsContainer
{
    public List<FullProduct> UserProducts
    {
        get => Products;
    }

    public void AddProduct(Product product, int amount)
    {
        if (FindProduct(product) != null)
            throw new Exception();
        Products.Add(new FullProduct(product, amount));
    }
}