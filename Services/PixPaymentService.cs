using ProvaPub.Enums;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services;

public class PixPaymentService : IPaymentService

{
    public PaymentMethod SupportedPaymentMethod => PaymentMethod.Pix;

    public Task ProcessPayment(decimal paymentValue, int customerId)
    {
        // Implementação específica para pagamento via PIX
        return Task.CompletedTask;
    }
}
