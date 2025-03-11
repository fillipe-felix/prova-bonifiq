using ProvaPub.Enums;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Factories;

public class PaymentServiceFactory
{
    private readonly IEnumerable<IPaymentService> _paymentServices;

    public PaymentServiceFactory(IEnumerable<IPaymentService> paymentServices)
    {
        _paymentServices = paymentServices;
    }

    /// <summary>
    /// Retrieves the appropriate payment service that supports the specified payment method.
    /// </summary>
    /// <param name="paymentMethod">The payment method for which the service is being requested.</param>
    /// <returns>The implementation of <see cref="IPaymentService"/> that supports the specified payment method.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified payment method is not supported.</exception>
    public IPaymentService GetPaymentService(PaymentMethod paymentMethod)
    {
        var paymentService = _paymentServices.FirstOrDefault(p => 
            p.SupportedPaymentMethod == paymentMethod);
        
        if (paymentService == null)
            throw new ArgumentException($"Payment method '{paymentMethod}' not supported.");
            
        return paymentService;
    }


}
