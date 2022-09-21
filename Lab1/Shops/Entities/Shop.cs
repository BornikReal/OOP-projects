﻿using Shops.Models;
using Shops.Products;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ProductsGroup> _products;
    private readonly GeneratorId _generatorId;
    public Shop(string name, string address)
    {
        Name = name;
        Adress = address;
        _products = new List<ProductsGroup>();
        _generatorId = new GeneratorId("ShopsId.json");
        Id = _generatorId.Generate();
        Wallet = 0;
    }

    public IReadOnlyList<ProductsGroup> Products => _products;
    public string Name { get; set; }
    public string Adress { get; set; }
    public decimal Wallet { get; private set; }
    public int Id { get; }
    public void RemoveProducts(ProductsGroup new_products)
    {
        if (new_products.Shop == this)
        {
            _products.Remove(new_products);
            new_products.Shop = null;
        }
        else
        {
            throw new Exception();
        }
    }

    public void AddProducts(ProductsGroup new_products)
    {
        if (new_products.Shop == null)
        {
            ProductsGroup? productsGroup = _products.Find(s => s.Product == new_products.Product);
            if (productsGroup == null)
            {
                _products.Add(new_products);
                new_products.Shop = this;
            }
            else if (productsGroup == new_products)
            {
                productsGroup.Amount += new_products.Amount;
            }
            else
            {
                throw new Exception();
            }
        }
        else
        {
            throw new Exception();
        }
    }

    public ProductsGroup? FindProducts(ProductsGroup products)
    {
        return _products.Find(s => s == products);
    }

    public ProductsGroup? GetProductsGroup(Product product)
    {
        return _products.Find(s => s.Product == product);
    }

    public void Buy(Person person, int productID, int amount)
    {
        ProductsGroup? new_product = _products.Find(s => s.Product.Id == productID);
        if (new_product == null)
            throw new Exception();
        if (new_product.Amount < amount)
            throw new Exception();
        if (person.Wallet < new_product.GetPrice(amount))
            throw new Exception();
        person.Wallet -= new_product.GetPrice(amount);
        Wallet += new_product.GetPrice(amount);
        new_product.Amount -= amount;
    }
}