using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dtos;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(long id)
        {
            var vacio = new CustomerDto();

            return new OkObjectResult(vacio);
            
        }

        [HttpGet]
        
        public async Task<List<CustomerDto>> GetCustomers()
        {
            throw new NotImplementedException();

        }

        [HttpDelete("{id}")]

        public async Task<bool> DeleteCustomer(long id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        

        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            var vacio = new CustomerDto();

            return new CreatedResult($"https://localhost:7180/api/customer/{vacio.Id}", null);

            //throw new NotImplementedException();
        }

        [HttpPut]

        public async Task<CustomerDto> UpdateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
