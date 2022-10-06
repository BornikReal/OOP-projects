using Shops.Entities;
using Shops.Models;
using Shops.Products;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopsTest
{
    private readonly ShopManager _shopManager;
    public ShopsTest()
    {
        _shopManager = new ShopManager();
    }

    [Fact]
    public void RegisterShopAndProductAndChangePrice()
    {
        var shop = new Shop("LSDstore");
        _shopManager.RegisterShop(shop);
        var product = new Product("Cool pills");
        _shopManager.RegisterProduct(product);
        shop.ProductsContainer.AddProduct(product, 10, 2);
        _shopManager.SetNewPrice(shop, product, 20);
        Assert.Equal(20, shop.ProductsContainer.FindProduct(product) !.SinglePrice);
    }

    [Fact]
    public void RegisterSeveralShopsAndBuyCheapestProduct()
    {
        var product = new Product("Cool substances");
        _shopManager.RegisterProduct(product);

        var shop1 = new Shop("LSDstore");
        _shopManager.RegisterShop(shop1);
        shop1.ProductsContainer.AddProduct(product, 10, 2);

        var shop2 = new Shop("CocainStore");
        _shopManager.RegisterShop(shop2);
        shop2.ProductsContainer.AddProduct(product, 20, 6);

        var shop3 = new Shop("FunnyMushroomsStore");
        _shopManager.RegisterShop(shop3);
        shop3.ProductsContainer.AddProduct(product, 15, 4);

        var person = new Person("BornikReal", new CashAccount(1000));

        _shopManager.BuyCheapest(person, product, 3);

        Assert.Equal(2, shop1.ProductsContainer.FindProduct(product) !.Amount);
        Assert.Equal(6, shop2.ProductsContainer.FindProduct(product) !.Amount);
        Assert.Equal(1, shop3.ProductsContainer.FindProduct(product) !.Amount);
    }
}
