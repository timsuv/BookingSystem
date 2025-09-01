using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestaurantDbContext _context;
        public CustomerRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers
                .OrderBy(c => c.CustomerName)
                .ToListAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> Create(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Delete(int id)
        {
            var customer = await GetById(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer?> GetByPhoneNumber(string phoneNumber)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Phone == phoneNumber);
        }

        public async Task<bool> HasActiveBookings(int customerId)
        {
            return await _context.Bookings
                .AnyAsync(b => b.CustomerId == customerId && b.BookingDate >= DateTime.Today);
        }
    }
}
