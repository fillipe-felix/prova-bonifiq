using ProvaPub.Enums;

namespace ProvaPub.Services.Interfaces;

public interface IPaymentService
{
    PaymentMethod SupportedPaymentMethod { get; }
    Task ProcessPayment(decimal paymentValue, int customerId);
}
