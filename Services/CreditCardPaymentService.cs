using ProvaPub.Enums;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services;

public class CreditCardPaymentService : IPaymentService

{
    public PaymentMethod SupportedPaymentMethod => PaymentMethod.CreditCard;
    
    public Task ProcessPayment(decimal paymentValue, int customerId)
    {
        // Implementação específica para pagamento via cartão de crédito
        return Task.CompletedTask;
    }
}
