namespace ProvaPub.Models;

public class ProductList : PagedList<Product>
{
    public List<Product> Products
    {
        get => Items;
        set => Items = value;
    }
}
