namespace Shops.Products;

public class UserProductsContainer : ProductsContainer
{
    public void AddProduct(Product product, int amount)
    {
        if (FindProduct(product) != null)
            throw new Exception();
        ElementsDirector.Builder = ProductsGroupBuilder;
        ElementsDirector.AddReadProductGroups(product, amount);
        Products.Add(ProductsGroupBuilder.GetProductsGroup());
    }
}