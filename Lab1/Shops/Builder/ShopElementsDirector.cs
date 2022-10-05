using Shops.Entities;
using Shops.Products;

namespace Shops.Builder;

public class ShopElementsDirector
{
    public IShopElementsBuilder? Builder { private get; set; }

    public void AddEmtpyProductGroups(Product product, decimal price)
    {
        Builder!.ProductsGroupBuildProduct(product);
        Builder.ProductsGroupBuildPrice(price);
    }

    public void AddEmtpyShopProductGroups(Product product, decimal price, Shop shop)
    {
        Builder!.ProductsGroupBuildProduct(product);
        Builder.ProductsGroupBuildPrice(price);
        Builder.ProductsGroupBuildShop(shop);
    }

    public void AddProductGroups(Product product, decimal price, int amount, Shop shop)
    {
        Builder!.ProductsGroupBuildProduct(product);
        Builder.ProductsGroupBuildPrice(price);
        Builder.ProductsGroupBuildPrice(amount);
        Builder.ProductsGroupBuildShop(shop);
    }
}
