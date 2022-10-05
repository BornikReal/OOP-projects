using Shops.Entities;
using Shops.Products;

namespace Shops.Builder;

public class ShopBuilder : IShopElementsBuilder
{
    private Shop _shop = new Shop();

    public void ShopBuildName(string name)
    {
        _shop.Name = name;
    }

    public void ShopBuildProducts(ShopProductsContainer shopProductsContainer)
    {
        _shop.ProductsContainer = shopProductsContainer;
    }

    public void Reset()
    {
        _shop = new Shop();
    }

    public Shop GetShop()
    {
        Shop shop = _shop;
        _shop = new Shop();
        return shop;
    }

    public void ProductsGroupBuildAmount(int amount)
    {
        throw new NotImplementedException();
    }

    public void ProductsGroupBuildPrice(decimal price)
    {
        throw new NotImplementedException();
    }

    public void ProductsGroupBuildProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public void ProductsGroupBuildShop(Shop shop)
    {
        throw new NotImplementedException();
    }
}