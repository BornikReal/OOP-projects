using Shops.Entities;
using Shops.Products;

namespace Shops.Builder;

public interface IShopElementsBuilder
{
    void ProductsGroupBuildPrice(decimal price);
    void ProductsGroupBuildAmount(int amount);
    void ProductsGroupBuildProduct(Product product);
    void ProductsGroupBuildShop(Shop shop);
    public void Reset();
}
