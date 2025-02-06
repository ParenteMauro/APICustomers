using CustomerApi.CasosDeUsos;
using CustomerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dtos;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {

        private readonly CustomerDataBaseContext _customerDataBaseContext;
        
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;

        public CustomerController(CustomerDataBaseContext customerDataBaseContext, IUpdateCustomerUseCase updateCustomerUseCase)
        {
            _customerDataBaseContext = customerDataBaseContext;
            _updateCustomerUseCase = updateCustomerUseCase;
        }
        
            

            


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(long id)
        {
            CustomerEntity customer = await _customerDataBaseContext.Get(id);

            return new OkObjectResult(customer.ToDto());
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomers()
        {
            List<CustomerDto> listCustomers =  _customerDataBaseContext.Customer.Select(customer=>customer.ToDto()).ToList();

            return new OkObjectResult(listCustomers);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
           bool result = await _customerDataBaseContext.Delete(id);
           return new OkObjectResult(result); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
       
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result =  await _customerDataBaseContext.Add(customer);

            0return new CreatedResult($"https://localhost:7180/api/customer/{result.Id}", null);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await _updateCustomerUseCase.Execute(customer);
            if(result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }
    }
}
