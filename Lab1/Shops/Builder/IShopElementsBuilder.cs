using Shops.Entities;
using Shops.Models;
using Shops.Products;

namespace Shops.Builder;

public interface IShopElementsBuilder
{
    void ProductsGroupBuildPrice(decimal price);
    void ProductsGroupBuildAmount(int amount);
    void ProductsGroupBuildProduct(Product product);
    void ProductsGroupBuildShop(Shop shop);
    void ShopBuildName(string name);
    void ShopBuildProducts(ShopProductsContainer shopProductsContainer);
    void PersonBuildName(string name);
    void PersonBuildWallet(CashAccount wallet);
    public void Reset();
}
