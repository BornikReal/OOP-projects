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
        ElementsDirector.Builder = ProductsGroupBuilder;
        ElementsDirector.MakeReadProductGroups(product, amount);
        Products.Add(ProductsGroupBuilder.GetProductsGroup());
    }
}