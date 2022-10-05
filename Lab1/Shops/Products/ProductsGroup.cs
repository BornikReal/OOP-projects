using Shops.Builder;

namespace Shops.Products;

public class ProductsGroup
{
    private readonly List<ShopProducts> _products = new List<ShopProducts>();
    private readonly ShopElementsDirector _elementsDirector = new ShopElementsDirector();
    private readonly ProductsGroupBuilder _productsGroupBuilder = new ProductsGroupBuilder();
    public IReadOnlyList<ShopProducts> Products => _products;

    public ShopProducts? FindProduct(Product product)
    {
        return _products.Find(s => s.Product == product);
    }

    public void AddProduct(Product product, int amount)
    {
        if (FindProduct(product) != null)
            throw new Exception();
        _elementsDirector.Builder = _productsGroupBuilder;
        _elementsDirector.AddReadProductGroups(product, amount);
        _products.Add(_productsGroupBuilder.GetProductsGroup());
    }

    public void RemoveProduct(Product product)
    {
        ShopProducts? removable = FindProduct(product);
        if (removable == null)
            throw new Exception();
        _products.Remove(removable);
    }
}
