using Shops.Builder;
using Shops.Products;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ProductsGroup> _products = new List<ProductsGroup>();
    private readonly ShopElementsDirector _elementsDirector = new ShopElementsDirector();
    private readonly ProductsGroupBuilder _productsGroupBuilder = new ProductsGroupBuilder();
    public Shop(string name, string address)
    {
        Name = name;
        Adress = address;
    }

    public IReadOnlyList<ProductsGroup> Products => _products;
    public string Name { get; set; }
    public string Adress { get; set; }
    public Guid Id { get; } = Guid.NewGuid();
    public void RemoveProducts(Product product)
    {
        ProductsGroup? products = FindProduct(product);
        if (products == null)
            throw new Exception();
        _products.Remove(products);
    }

    public void AddProducts(Product new_product, decimal price, int amount)
    {
        if (FindProduct(new_product) != null)
            throw new Exception();
        _elementsDirector.Builder = _productsGroupBuilder;
        _elementsDirector.AddProductGroups(new_product, price, amount, this);
        _products.Add(_productsGroupBuilder.GetProductsGroup());
    }

    public ProductsGroup? FindProduct(Product product)
    {
        return _products.Find(s => s.Product == product);
    }

    public void Buy(Person person, Product product, int amount)
    {
        ProductsGroup? products = FindProduct(product);
        if (products == null)
            throw new Exception();
        if (person.Wallet < products.GetPrice(amount))
            throw new Exception();
        products.RemoveProducts(amount);
    }

    public bool Equals(Shop obj)
    {
        return Id == obj.Id;
    }
}