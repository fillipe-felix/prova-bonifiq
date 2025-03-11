using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces;

public interface IOrderRepository
{
    /// <summary>
    /// Asynchronously creates a new order and saves it to the database.
    /// </summary>
    /// <param name="order">The order to be created and persisted in the database.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateOrderAsync(Order order);
}
