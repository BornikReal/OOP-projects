using Shops.Entities;

namespace Shops.Exception.ShopManagerException;

public class ShopAlreadyRegisteredException : ShopManagerException
{
    public ShopAlreadyRegisteredException(Shop shop)
        : base($"Shop with name \"{shop.Name}\" and Id \"{shop.Id}\" already registered!")
    { }
}
