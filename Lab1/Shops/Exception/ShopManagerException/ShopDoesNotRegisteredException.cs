using Shops.Entities;

namespace Shops.Exception.ShopManagerException;

public class ShopDoesNotRegisteredException : ShopManagerException
{
    public ShopDoesNotRegisteredException(Shop shop)
        : base($"Shop with name \"{shop.Name}\" and Id \"{shop.Id}\" doesn't registered!")
    { }
}
