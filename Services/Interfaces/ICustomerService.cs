using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces;

public interface ICustomerService
{
    /// <summary>
    /// Retrieves a paginated list of customers.
    /// </summary>
    /// <param name="page">The page number to retrieve. Must be a positive integer.</param>
    /// <returns>A CustomerList containing the list of customers, total count of customers, and an indicator if there are more pages.</returns>
    CustomerList ListCustomers(int page);

    /// <summary>
    /// Determines whether a customer can make a purchase based on business rules.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer attempting the purchase.</param>
    /// <param name="purchaseValue">The monetary value of the purchase.</param>
    /// <returns>Returns a boolean indicating whether the customer is allowed to make the purchase.</returns>
    Task<bool> CanPurchase(int customerId, decimal purchaseValue);
}
