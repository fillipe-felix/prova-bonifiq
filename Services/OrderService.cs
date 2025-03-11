using ProvaPub.Enums;
using ProvaPub.Factories;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly PaymentServiceFactory _paymentServiceFactory;

		public OrderService(PaymentServiceFactory paymentServiceFactory, IOrderRepository orderRepository)
		{
			_paymentServiceFactory = paymentServiceFactory;
			_orderRepository = orderRepository;
		}


        public async Task<Order> PayOrder(PaymentMethod paymentMethod, decimal paymentValue, int customerId)
		{
			var paymentService = _paymentServiceFactory.GetPaymentService(paymentMethod);
			await paymentService.ProcessPayment(paymentValue, customerId);

			var order = new Order
			{
				Value = paymentValue,
				CustomerId = customerId,
				OrderDate = DateTime.UtcNow // Salvando em UTC
			};

			await InsertOrder(order);
			return order;
		}

		private async Task InsertOrder(Order order)
        {
			//Insere pedido no banco de dados
			await _orderRepository.CreateOrderAsync(order);
        }
	}
}
