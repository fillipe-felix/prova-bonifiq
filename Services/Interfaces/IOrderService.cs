using ProvaPub.Enums;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces;

public interface IOrderService
{
    Task<Order> PayOrder(PaymentMethod paymentMethod, decimal paymentValue, int customerId);
}
