using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces;

public interface IProductService
{
    /// <summary>
    /// Retrieves a paginated list of products.
    /// </summary>
    /// <param name="page">The page number to retrieve. Must be greater than or equal to 1.</param>
    /// <returns>A <see cref="ProductList"/> containing the list of products on the specified page,
    /// the total count of products, and whether there are more pages available.</returns>
    ProductList ListProducts(int page);
}
