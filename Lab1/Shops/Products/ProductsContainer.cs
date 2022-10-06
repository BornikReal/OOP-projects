using Shops.Builder;

namespace Shops.Products;

public abstract class ProductsContainer
{
    protected List<FullProduct> Products { get; } = new List<FullProduct>();
    protected ShopElementsDirector ElementsDirector { get; } = new ShopElementsDirector();
    protected ShopProductBuilder ProductsGroupBuilder { get; } = new ShopProductBuilder();

    public FullProduct? FindProduct(Product product)
    {
        return Products.Find(s => s.Product == product);
    }

    public void RemoveProduct(Product product)
    {
        FullProduct? removable = FindProduct(product);
        if (removable == null)
            throw new Exception();
        Products.Remove(removable);
    }
}
