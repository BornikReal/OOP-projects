﻿using Shops.Exception.ProductsContainerException;
using Shops.Products.ConcreteProduct;

namespace Shops.Products.ProductsContainers;

public class UserProductsContainer : ProductsContainer
{
    public List<FullProduct> UserProducts
    {
        get => Products;
    }

    public void AddProduct(Product product, int amount)
    {
        if (FindProduct(product) != null)
            throw new ProductAlreadyExist(product);
        Products.Add(new FullProduct(product, amount));
    }
}