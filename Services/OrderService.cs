using ProvaPub.Enums;
using ProvaPub.Factories;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class OrderService : IOrderService
	{
		private readonly TestDbContext _ctx;
		private readonly PaymentServiceFactory _paymentServiceFactory;

		public OrderService(TestDbContext ctx, PaymentServiceFactory paymentServiceFactory)
		{
			_ctx = ctx;
			_paymentServiceFactory = paymentServiceFactory;
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

			return await InsertOrder(order);
		}

		private async Task<Order> InsertOrder(Order order)
        {
			//Insere pedido no banco de dados
			return (await _ctx.Orders.AddAsync(order)).Entity;
        }
	}
}
