using Shops.Entities;
using Shops.Products;

namespace Shops.Builder;

public class ShopElementsDirector
{
    public IShopElementsBuilder? Builder { private get; set; }

    public void AddReadProductGroups(Product product, int amount)
    {
        Builder!.ProductsGroupBuildProduct(product);
        Builder.ProductsGroupBuildPrice(amount);
    }

    public void AddProductGroups(Product product, decimal price, int amount, Shop shop)
    {
        Builder!.ProductsGroupBuildProduct(product);
        Builder.ProductsGroupBuildPrice(price);
        Builder.ProductsGroupBuildPrice(amount);
        Builder.ProductsGroupBuildShop(shop);
    }

    public void AddEmptyShop(string name)
    {
        Builder!.ShopBuildName(name);
    }

    public void AddReadyShop(string name, ShopProductsContainer shopProductsContainer)
    {
        Builder!.ShopBuildName(name);
        Builder.ShopBuildProducts(shopProductsContainer);
    }
}
