using BookingSystem.Models;

namespace BookingSystem.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetById(int id);
        Task<Customer> Create(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<bool> Delete(int id);
        Task<Customer?> GetByPhoneNumber(string phoneNumber);
        Task<bool> HasActiveBookings(int customerId);
    }
}
