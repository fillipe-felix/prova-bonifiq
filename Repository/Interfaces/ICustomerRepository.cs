using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces;

public interface ICustomerRepository
{
    /// Retrieves all customers from the database.
    /// <return>Returns an IQueryable of Customer objects, allowing further query operations to be performed.</return>
    IQueryable<Customer?> GetAll();

    /// <summary>
    /// Retrieves a customer by their unique identifier asynchronously.
    /// </summary>
    /// <param name="customerId">An integer representing the unique identifier of the customer to be retrieved.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the customer instance
    /// if found, or null if no customer with the specified identifier exists.
    /// </returns>
    Task<Customer?> GetByIdAsync(int customerId);

    /// <summary>
    /// Checks whether a customer has made a purchase within the last month from the specified base date.
    /// </summary>
    /// <param name="customerId">The identifier of the customer whose purchase history is being checked.</param>
    /// <param name="baseDate">The reference date to calculate the last month period. Purchases made after this date minus one month are considered.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the customer has made a purchase in the last month.</returns>
    Task<bool> HasPurchasedInLastMonthAsync(int customerId, DateTime baseDate);

    /// <summary>
    /// Determines whether a customer has ever made a purchase.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>Returns a task representing the asynchronous operation. The task result is true if the customer has made a purchase before; otherwise, false.</returns>
    Task<bool> HasEverPurchasedAsync(int customerId);
}
