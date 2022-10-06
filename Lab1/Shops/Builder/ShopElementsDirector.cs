using Shops.Entities;
using Shops.Models;
using Shops.Products;

namespace Shops.Builder;

public class ShopElementsDirector
{
    public IShopElementsBuilder? Builder { private get; set; }

    public void MakeReadProductGroups(Product product, int amount)
    {
        Builder!.ProductsGroupBuildProduct(product);
        Builder.ProductsGroupBuildPrice(amount);
    }

    public void MakeProductGroups(Product product, decimal price, int amount, Shop shop)
    {
        Builder!.ProductsGroupBuildProduct(product);
        Builder.ProductsGroupBuildPrice(price);
        Builder.ProductsGroupBuildPrice(amount);
        Builder.ProductsGroupBuildShop(shop);
    }

    public void MakeEmptyShop(string name)
    {
        Builder!.ShopBuildName(name);
    }

    public void MakeReadyShop(string name, ShopProductsContainer shopProductsContainer)
    {
        Builder!.ShopBuildName(name);
        Builder.ShopBuildProducts(shopProductsContainer);
    }

    public void MakePersonNewWallet(string name, decimal price)
    {
        Builder!.PersonBuildName(name);
        Builder.PersonBuildWallet(new CashAccount(price));
    }

    public void MakePersonReadyWallet(string name, CashAccount account)
    {
        Builder!.PersonBuildName(name);
        Builder.PersonBuildWallet(account);
    }
}
