using Shops.Products;

namespace Shops.Builder;

public interface IShopElementsBuilder
{
    void ProductsGroupBuildPrice(decimal price);
    void ProductsGroupBuildAmount(int amount);
    void ProductsGroupBuildProduct(Product product);
    public void Reset();
}
