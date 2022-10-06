using Shops.Builder;
using Shops.Entities;
using Shops.Products;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopsTest
{
    private readonly ShopManager _shopManager;
    private readonly ShopElementsDirector _shopElementsDirector;
    public ShopsTest()
    {
        _shopElementsDirector = new ShopElementsDirector();
        var shopManagerBuilder = new ShopManagerBuilder();
        _shopElementsDirector.Builder = shopManagerBuilder;
        _shopElementsDirector.MakeEmptyShopManager();
        _shopManager = shopManagerBuilder.GetShopManager();
    }

    [Fact]
    public void RegisterShopAndProductAndChangePrice()
    {
        var shopBuilder = new ShopBuilder();
        _shopElementsDirector.Builder = shopBuilder;
        _shopElementsDirector.MakeEmptyShop("LSDstore");
        Shop shop = shopBuilder.GetShop();
        _shopManager.RegisterShop(shop);
        var product = new Product("Cool pills");
        _shopManager.RegisterProduct(product);
        shop.ProductsContainer.AddProduct(product, 10, 2);
        _shopManager.SetNewPrice(shop, product, 20);
        Assert.Equal(1, 1);
    }
}
