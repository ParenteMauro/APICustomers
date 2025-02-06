
using CustomerApi.Repositories;
using WebApplication2.Controllers;
using WebApplication2.Dtos;

namespace CustomerApi.CasosDeUsos
{
    public interface IUpdateCustomerUseCase
    {
        Task<CustomerDto> Execute (CustomerDto customer);   
    }
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly CustomerDataBaseContext _customerDataBaseContext;
        
        public UpdateCustomerUseCase(CustomerDataBaseContext customerDataBaseContext)
        {
            _customerDataBaseContext = customerDataBaseContext;
        }
        public async Task<CustomerDto?> Execute(CustomerDto customer)
        {
            var entity =  await _customerDataBaseContext.Get(customer.Id);
            if (entity == null)
            {
                return null;
            }
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;
            await _customerDataBaseContext.Update(entity);
            return entity.ToDto();
        }
    }
}
