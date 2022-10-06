using Shops.Entities;
using Shops.Models;
using Shops.Products;

namespace Shops.Builder;

public class FullProductBuilder : IShopElementsBuilder
{
    private FullProduct _productsGroup = new FullProduct();

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
        _productsGroup = new FullProduct();
    }

    public FullProduct GetProductsGroup()
    {
        FullProduct productsGroup = _productsGroup;
        _productsGroup = new FullProduct();
        return productsGroup;
    }

    public void ShopBuildName(string name)
    {
        throw new NotImplementedException();
    }

    public void ShopBuildProducts(ShopProductsContainer? shopProductsContainer)
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

    public void ShopManagerBuildShops(List<Shop> shops)
    {
        throw new NotImplementedException();
    }

    public void ShopManagerBuildProducts(List<Product> products)
    {
        throw new NotImplementedException();
    }
}
