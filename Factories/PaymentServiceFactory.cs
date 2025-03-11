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
    
    public IPaymentService GetPaymentService(PaymentMethod paymentMethod)
    {
        var paymentService = _paymentServices.FirstOrDefault(p => 
            p.SupportedPaymentMethod == paymentMethod);
        
        if (paymentService == null)
            throw new ArgumentException($"Payment method '{paymentMethod}' not supported.");
            
        return paymentService;
    }


}
