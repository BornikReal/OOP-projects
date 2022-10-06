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
        Builder.ShopBuildProducts(null);
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

    public void MakeEmptyShopManager()
    {
        Builder!.ShopManagerBuildProducts(new List<Product>());
        Builder.ShopManagerBuildShops(new List<Shop>());
    }

    public void MakeFullShopManager(List<Product> products, List<Shop> shops)
    {
        Builder!.ShopManagerBuildProducts(products);
        Builder.ShopManagerBuildShops(shops);
    }
}
