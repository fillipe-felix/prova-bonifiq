using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// O Código abaixo faz uma chmada para a regra de negócio que valida se um consumidor pode fazer uma compra.
	/// Crie o teste unitário para esse Service. Se necessário, faça as alterações no código para que seja possível realizar os testes.
	/// Tente criar a maior cobertura possível nos testes.
	/// 
	/// Utilize o framework de testes que desejar. 
	/// Crie o teste na pasta "Tests" da solution
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte4Controller :  ControllerBase
	{
        private readonly ICustomerService _customerService;

        public Parte4Controller(ICustomerService customerService)
        {
	        _customerService = customerService;
        }

        /// <summary>
        /// Determines whether a customer is eligible to make a purchase based on their ID and the purchase value.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <param name="purchaseValue">The value of the purchase the customer wants to make.</param>
        /// <returns>
        /// A boolean indicating whether the customer can make the purchase.
        /// Returns <c>true</c> if the customer can proceed with the purchase; otherwise, <c>false</c>.
        /// </returns>
        /// <response code="200">Returns whether the customer can make the purchase.</response>
        /// <response code="400">If the input parameters are invalid.</response>
        /// <response code="500">If an unexpected error occurs during processing.</response>
        [HttpGet("CanPurchase")]
		public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
		{
			return await _customerService.CanPurchase(customerId, purchaseValue);
		}
	}
}
