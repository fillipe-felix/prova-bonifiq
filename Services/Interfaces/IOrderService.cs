using ProvaPub.Enums;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces;

public interface IOrderService
{
    /// <summary>
    /// Processes the payment for an order using the specified payment method and creates an order record.
    /// </summary>
    /// <param name="paymentMethod">The payment method to be used for processing the payment (e.g., Pix, CreditCard, PayPal).</param>
    /// <param name="paymentValue">The monetary value of the order.</param>
    /// <param name="customerId">The unique identifier of the customer making the order.</param>
    /// <returns>Returns the created <see cref="Order"/> object, which includes details such as value, customer, and order date.</returns>
    Task<Order> PayOrder(PaymentMethod paymentMethod, decimal paymentValue, int customerId);
}
