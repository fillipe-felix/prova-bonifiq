using ProvaPub.Enums;
using ProvaPub.Factories;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class OrderService : IOrderService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly PaymentServiceFactory _paymentServiceFactory;

		public OrderService(PaymentServiceFactory paymentServiceFactory, IOrderRepository orderRepository, ICustomerRepository customerRepository)
		{
			_paymentServiceFactory = paymentServiceFactory;
			_orderRepository = orderRepository;
			_customerRepository = customerRepository;
		}


        public async Task<Order> PayOrder(PaymentMethod paymentMethod, decimal paymentValue, int customerId)
		{
			var customer = await _customerRepository.GetByIdAsync(customerId);
			if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");
			
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
