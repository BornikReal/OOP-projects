using Shops.Entities;
using Shops.Products;

namespace Shops.Builder;

public class ProductsGroupBuilder : IShopElementsBuilder
{
    private ProductsGroup _productsGroup = new ProductsGroup();

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
        _productsGroup = new ProductsGroup();
    }

    public ProductsGroup GetProductsGroup()
    {
        ProductsGroup productsGroup = _productsGroup;
        _productsGroup = new ProductsGroup();
        return productsGroup;
    }
}
