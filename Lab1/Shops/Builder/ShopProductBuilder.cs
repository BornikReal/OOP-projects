using Shops.Entities;
using Shops.Models;
using Shops.Products;

namespace Shops.Builder;

public class ShopProductBuilder : IShopElementsBuilder
{
    private ShopProduct _productsGroup = new ShopProduct();

    public void ProductsGroupBuildPrice(decimal price)
    {
        _productsGroup.SinglePrice = price;
    }

    public void ProductsGroupBuildProduct(Product product)
    {
        _productsGroup.Product = product;
    }

    public void ProductsGroupBuildAmount(int amount)
    {
        _productsGroup.Amount = amount;
    }

    public void ProductsGroupBuildShop(Shop shop)
    {
        _productsGroup.Shop = shop;
    }

    public void Reset()
    {
        _productsGroup = new ShopProduct();
    }

    public ShopProduct GetProductsGroup()
    {
        ShopProduct productsGroup = _productsGroup;
        _productsGroup = new ShopProduct();
        return productsGroup;
    }

    public void ShopBuildName(string name)
    {
        throw new NotImplementedException();
    }

    public void ShopBuildProducts(ShopProductsContainer shopProductsContainer)
    {
        throw new NotImplementedException();
    }

    public void PersonBuildName(string name)
    {
        throw new NotImplementedException();
    }

    public void PersonBuildWallet(CashAccount wallet)
    {
        throw new NotImplementedException();
    }
}
