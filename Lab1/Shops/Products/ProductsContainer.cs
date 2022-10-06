using Shops.Builder;

namespace Shops.Products;

public abstract class ProductsContainer
{
    protected List<ShopProduct> Products { get; } = new List<ShopProduct>();
    protected ShopElementsDirector ElementsDirector { get; } = new ShopElementsDirector();
    protected ShopProductBuilder ProductsGroupBuilder { get; } = new ShopProductBuilder();

    public ShopProduct? FindProduct(Product product)
    {
        return Products.Find(s => s.Product == product);
    }

    public void RemoveProduct(Product product)
    {
        ShopProduct? removable = FindProduct(product);
        if (removable == null)
            throw new Exception();
        Products.Remove(removable);
    }
}
