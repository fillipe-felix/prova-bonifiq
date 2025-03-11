using ProvaPub.Enums;

namespace ProvaPub.Services.Interfaces;

public interface IPaymentService
{
    /// <summary>
    /// Gets the payment method supported by the current implementation of the payment service.
    /// </summary>
    /// <remarks>
    /// This property indicates the specific payment method (e.g., Pix, CreditCard, PayPal)
    /// that the implementing payment service supports. It is used to match the appropriate
    /// payment service with the desired payment method.
    /// </remarks>
    PaymentMethod SupportedPaymentMethod { get; }

    /// <summary>
    /// Processes the payment for a specified customer.
    /// </summary>
    /// <param name="paymentValue">The amount to be paid.</param>
    /// <param name="customerId">The identifier of the customer making the payment.</param>
    /// <returns>A task representing the asynchronous operation of processing the payment.</returns>
    Task ProcessPayment(decimal paymentValue, int customerId);
}
