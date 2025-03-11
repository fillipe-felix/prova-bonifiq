using ProvaPub.Enums;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services;

public class PaypalPaymentService : IPaymentService

{
    public PaymentMethod SupportedPaymentMethod => PaymentMethod.PayPal;

    public Task ProcessPayment(decimal paymentValue, int customerId)
    {
        // Implementação específica para pagamento via PayPal
        return Task.CompletedTask;
    }
}
