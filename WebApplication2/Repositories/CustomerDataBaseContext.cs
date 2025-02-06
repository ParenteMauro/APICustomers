using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Dtos;

namespace CustomerApi.Repositories
{
    public class CustomerDataBaseContext : DbContext
    {
        public CustomerDataBaseContext(DbContextOptions<CustomerDataBaseContext> options) 
            : base(options) 
        { 

        }
        
        public DbSet<CustomerEntity> Customer { get; set; }

        public async Task<CustomerEntity?> Get(long id)
        {
            return await Customer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address
            };
            EntityEntry<CustomerEntity> response = await Customer.AddAsync(entity);
            await SaveChangesAsync();

            return await Get(response.Entity.Id ?? throw new Exception("No se a podido guardar entidad"));
        }

        public async Task<bool> Update(CustomerEntity customerEntity)
        {
           Customer.Update(customerEntity);
           await SaveChangesAsync();
           return true;
        }

        public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            Customer.Remove(entity);
            await SaveChangesAsync();

            return true;

        }
    }

    [Table("Customers")]
    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

        public CustomerDto ToDto()
        {
            CustomerDto customerDto = new CustomerDto()
            {
                Id = this.Id ?? throw new Exception("Customer no encontrado"),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Address = Address,
                Phone = Phone

            };

            return customerDto;
        }
    }
}
