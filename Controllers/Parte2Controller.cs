using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		private readonly IProductService _productService;
		private readonly ICustomerService _customerService;
		
		/// <summary>
		/// Precisamos fazer algumas alterações:
		/// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
		/// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
		/// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
		/// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
		/// 
		/// </summary>
		public Parte2Controller(IProductService productService, ICustomerService customerService)
		{
			_productService = productService;
			_customerService = customerService;
		}
		
		/// <summary>
		/// Retrieves a paginated list of products.
		/// </summary>
		/// <param name="page">The page number to retrieve. Must be greater than or equal to 1.</param>
		/// <returns>A paginated list of products, including product details and whether there are more pages available.</returns>
		/// <remarks>
		/// Example of a paged result:
		/// 
		/// ```json
		/// {
		///     "items": [
		///         {
		///             "id": 1,
		///             "name": "Product A"
		///         },
		///         {
		///             "id": 2,
		///             "name": "Product B"
		///         }
		///     ],
		///     "totalCount": 50,
		///     "hasNext": true
		/// }
		/// ```
		/// </remarks>
		/// <response code="200">Returns the paginated list of products.</response>
		/// <response code="400">Returned if the page parameter is invalid.</response>
		[ProducesResponseType(typeof(ProductList), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet("products")]
		public ProductList ListProducts(int page)
		{
			return _productService.ListProducts(page);
		}

		/// <summary>
		/// Retrieves a paginated list of customers.
		/// </summary>
		/// <param name="page">The page number to retrieve. Must be greater than or equal to 1.</param>
		/// <returns>A paginated list of customers, including customer details and whether there are more pages available.</returns>
		/// <remarks>
		/// Example of a paged result:
		/// 
		/// ```json
		/// {
		///     "items": [
		///         {
		///             "id": 1,
		///             "name": "Customer A",
		///             "orders": []
		///         },
		///         {
		///             "id": 2,
		///             "name": "Customer B",
		///             "orders": []
		///         }
		///     ],
		///     "totalCount": 100,
		///     "hasNext": true
		/// }
		/// ```
		/// </remarks>
		/// <response code="200">Returns the paginated list of customers.</response>
		/// <response code="400">Returned if the page parameter is invalid.</response>
		[ProducesResponseType(typeof(CustomerList), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet("customers")]
		public CustomerList ListCustomers(int page)
		{
			return _customerService.ListCustomers(page);
		}
	}
}
