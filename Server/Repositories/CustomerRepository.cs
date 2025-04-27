using Microsoft.EntityFrameworkCore;
using Server.Models.DataModel;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CinemaContext _context;
        public CustomerRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return _context.Customers.FirstOrDefault(c => c.Id == customer.Id && c.UserId == customer.UserId && c.Phone == customer.Phone);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await GetAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
