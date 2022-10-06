using Shops.Entities;
using Shops.Models;
using Shops.Products;

namespace Shops.Builder;

public class PersonBuilder : IShopElementsBuilder
{
    private Person _person = new Person();
    public void PersonBuildName(string name)
    {
        _person.Name = name;
    }

    public void PersonBuildWallet(CashAccount wallet)
    {
        _person.Wallet = wallet;
    }

    public void Reset()
    {
        _person = new Person();
    }

    public Person GetPerson()
    {
        Person person = _person;
        _person = new Person();
        return person;
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

    public void ShopBuildProducts(ShopProductsContainer? shopProductsContainer)
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
