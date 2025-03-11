using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services;

public class ProductService : PagedListService, IProductService
{
    private readonly TestDbContext _ctx;

    public ProductService(TestDbContext ctx)
    {
        _ctx = ctx;
    }
    

    public ProductList ListProducts(int page)
    {
        var query = _ctx.Products;
        var products = GetPagedData(query, page, out int totalCount, out bool hasNext);
            
        return new ProductList
        {
            Products = products,
            TotalCount = totalCount,
            HasNext = hasNext
        };
    }
}
