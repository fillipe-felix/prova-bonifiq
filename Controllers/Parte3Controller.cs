using Microsoft.AspNetCore.Mvc;

using ProvaPub.Enums;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly IOrderService _orderService;

        public Parte3Controller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Places an order and processes the payment. Accepts multiple payment methods such as Pix, Credit Card, and PayPal.
        /// </summary>
        /// <param name="paymentMethod">The payment method used for the order (e.g., Pix=0, Credit Card=1, PayPal=2).</param>
        /// <param name="paymentValue">The amount to be paid for the order.</param>
        /// <param name="customerId">The ID of the customer placing the order.</param>
        /// <returns>
        /// A JSON object containing the order details with the local Brazilian time zone (UTC-3) applied to the OrderDate.
        /// Success returns HTTP 200 with the order details.
        /// Failure scenarios return appropriate HTTP status codes.
        /// </returns>
        /// <response code="200">Returned when the order is successfully processed.</response>
        /// <response code="400">Returned if the input parameters are invalid (e.g., negative payment value).</response>
        /// <response code="500">Returned if an internal server error occurs while processing the order.</response>
        [HttpGet("orders")]
        public async Task<IActionResult> PlaceOrder(PaymentMethod paymentMethod, decimal paymentValue, int customerId)
        {
            var order = await _orderService.PayOrder(paymentMethod, paymentValue, customerId);

            // Convertendo o OrderDate para o fuso horário brasileiro (UTC-3)
            var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var orderWithLocalTime = new
            {
                Id = order.Id,
                Value = order.Value,
                CustomerId = order.CustomerId,
                OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, brazilTimeZone),
            };

            return Ok(orderWithLocalTime);
        }
    }
}
