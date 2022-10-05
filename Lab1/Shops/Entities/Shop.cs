using Shops.Products;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ProductsGroup> _products = new List<ProductsGroup>();
    public Shop(string name, string address)
    {
        Name = name;
        Adress = address;
    }

    public IReadOnlyList<ProductsGroup> Products => _products;
    public string Name { get; set; }
    public string Adress { get; set; }
    public Guid Id { get; } = Guid.NewGuid();
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

    public void Buy(Person person, ProductsGroup product, int amount)
    {
        if (product.Amount < amount)
            throw new Exception();
        if (person.Wallet < product.GetPrice(amount))
            throw new Exception();
        person.Wallet -= product.GetPrice(amount);
        Wallet += product.GetPrice(amount);
        product.Amount -= amount;
    }

    public bool Equals(Shop obj)
    {
        return Name == obj.Name && Adress == obj.Adress;
    }
}