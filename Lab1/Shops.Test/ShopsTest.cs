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

        var person = new Person("Joseph Joestar", new CashAccount(1000));

        _shopManager.BuyCheapest(person, product, 3);

        Assert.Equal(2, shop1.ProductsContainer.FindProduct(product) !.Amount);
        Assert.Equal(6, shop2.ProductsContainer.FindProduct(product) !.Amount);
        Assert.Equal(1, shop3.ProductsContainer.FindProduct(product) !.Amount);
        Assert.Equal(955, person.Wallet.Wallet);
    }

    [Fact]
    public void RegisterShopAndSeveralProductsAndBuyThem()
    {
        var shop = new Shop("LSDstore");
        _shopManager.RegisterShop(shop);

        var product1 = new Product("Cool pills");
        _shopManager.RegisterProduct(product1);
        shop.ProductsContainer.AddProduct(product1, 10, 7);

        var product2 = new Product("Cool powder");
        _shopManager.RegisterProduct(product2);
        shop.ProductsContainer.AddProduct(product2, 20, 4);

        var product3 = new Product("Cool mushrooms");
        _shopManager.RegisterProduct(product3);
        shop.ProductsContainer.AddProduct(product3, 30, 2);

        var person = new Person("Madoka Kaname", new CashAccount(1337));
        var shoppingList = new UserProductsContainer();
        shoppingList.AddProduct(product1, 5);
        shoppingList.AddProduct(product2, 4);
        shoppingList.AddProduct(product3, 1);
        _shopManager.BuyProducts(person, shop, shoppingList);

        Assert.Equal(1177, person.Wallet.Wallet);

        Assert.Equal(2, shop.ProductsContainer.FindProduct(product1) !.Amount);
        Assert.Equal(0, shop.ProductsContainer.FindProduct(product2) !.Amount);
        Assert.Equal(1, shop.ProductsContainer.FindProduct(product3) !.Amount);
    }

    [Fact]
    public void BuyProductWhenNotEnoughMoney()
    {
        var shop = new Shop("LSDstore");
        _shopManager.RegisterShop(shop);

        var product = new Product("Cool pills");
        _shopManager.RegisterProduct(product);
        shop.ProductsContainer.AddProduct(product, 10, 7);

        var person = new Person("Mayuri Shiina", new CashAccount(69));
        Assert.ThrowsAny<Exception>(() => person.Wallet.Buy(shop, product, 7));
    }

    [Fact]
    public void BuyProductWhenNotEnoughProducts()
    {
        var shop = new Shop("LSDstore");
        _shopManager.RegisterShop(shop);

        var product = new Product("Cool pills");
        _shopManager.RegisterProduct(product);
        shop.ProductsContainer.AddProduct(product, 10, 7);

        var person = new Person("Lelouch Lamperouge", new CashAccount(228));
        Assert.ThrowsAny<Exception>(() => person.Wallet.Buy(shop, product, 10));
    }
}
