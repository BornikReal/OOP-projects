using Shops.Entities;

namespace Shops.Products;
public class ShopProductsContainer : ProductsContainer
{
    public void AddProduct(Product product, int price, int amount, Shop shop)
    {
        if (FindProduct(product) != null)
            throw new Exception();
        ElementsDirector.Builder = ProductsGroupBuilder;
        ElementsDirector.MakeProductGroups(product, price, amount, shop);
        Products.Add(ProductsGroupBuilder.GetProductsGroup());
    }

    public void ReplenishProducts(Product product, int amount)
    {
        FullProduct? products = FindProduct(product);
        if (products == null)
            throw new Exception();
        products.AddProducts(amount);
    }
}