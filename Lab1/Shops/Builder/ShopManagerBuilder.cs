using Shops.Entities;
using Shops.Models;
using Shops.Products;
using Shops.Services;

namespace Shops.Builder;

public class ShopManagerBuilder : IShopElementsBuilder
{
    private ShopManager _shopManager = new ShopManager();

    public void ShopManagerBuildProducts(List<Product> products)
    {
        _shopManager.InternalProducts = products;
    }

    public void ShopManagerBuildShops(List<Shop> shops)
    {
        _shopManager.InternalShops = shops;
    }

    public void Reset()
    {
        _shopManager = new ShopManager();
    }

    public ShopManager ShopManager()
    {
        ShopManager shopManager = _shopManager;
        _shopManager = new ShopManager();
        return shopManager;
    }

    public void PersonBuildName(string name)
    {
        throw new NotImplementedException();
    }

    public void PersonBuildWallet(CashAccount wallet)
    {
        throw new NotImplementedException();
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

    public void ShopBuildName(string name)
    {
        throw new NotImplementedException();
    }

    public void ShopBuildProducts(ShopProductsContainer shopProductsContainer)
    {
        throw new NotImplementedException();
    }
}
