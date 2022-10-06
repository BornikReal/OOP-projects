﻿using Shops.Models;
using Shops.Products;

namespace Shops.Entities;

public class Shop
{
    public Shop(string name)
    {
        Name = name;
        ProductsContainer = new ShopProductsContainer(this);
    }

    public ShopProductsContainer ProductsContainer { get; }

    public string Name { get; set; }

    public Guid Id { get; } = Guid.NewGuid();

    public void Buy(CashAccount acc)
    {
        if (acc.Sellable == null)
            throw new Exception();
        if (acc.Sellable.Shop != this)
            throw new Exception();
        acc.Sellable.Amount -= acc.SellAmount;
    }

    public bool Equals(Shop obj)
    {
        return Id == obj.Id;
    }
}