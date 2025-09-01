using BookingSystem.DTOs;
using BookingSystem.Models;
using BookingSystem.Repositories.CustomerRepository;

namespace BookingSystem.Services
{
    public interface ICustomerSevice
    {
        Task<IEnumerable<CustomerResponse>> GetAllCustomers();
        Task<CustomerResponse?> GetCustomerById(int id);
        Task<CustomerResponse?> CreateCustomer(CreateCustomerRequest request);
        Task<CustomerResponse?> UpdateCustomer(int id, UpdateCustomerRequest request);
        Task<bool> DeleteCustomer(int id);
        Task<Customer?> FindOrCreateCustomer(string name, string phone, string? email);

    }
    public class CustomerService : ICustomerSevice
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAll();
            return customers.Select(MapToResponse);
        }
        public async Task<CustomerResponse?> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetById(id);
            return customer != null ? MapToResponse (customer) : null;
        }

        public async Task<CustomerResponse?> CreateCustomer(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                CustomerName = request.Name,
                Phone = request.PhoneNumber,
                Email = request.EmailAddress
            };
            var createdCustomer = await _customerRepository.Create(customer);
            return MapToResponse(createdCustomer);
        }
        public async Task<CustomerResponse?> UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
                return null;

            customer.CustomerName = request.Name;
            customer.Phone = request.PhoneNumber;
            customer.Email = request.Email;

            var updatedCustomer = await _customerRepository.Update(customer);
            return MapToResponse(updatedCustomer);

        }
        public async Task<bool> DeleteCustomer(int id)
        {
            if (await _customerRepository.HasActiveBookings(id))
                return false;

            return await _customerRepository.Delete(id);
        }
        public async Task<Customer?> FindOrCreateCustomer(string name, string phone, string? email)
        {
            var existingCustomer = await _customerRepository.GetByPhoneNumber(phone);
            if (existingCustomer != null)
            {
                bool hasChanges = existingCustomer.CustomerName != name || existingCustomer.Email != email;
                if (hasChanges)
                {
                    existingCustomer.CustomerName = name;
                    existingCustomer.Email = email;
                    return await _customerRepository.Update(existingCustomer);
                }
                return existingCustomer;
            }


            var newCustomer = new Customer
            {
                CustomerName = name,
                Phone = phone,
                Email = email
            };
            return await _customerRepository.Create(newCustomer);
        }

        private CustomerResponse MapToResponse(Customer customer)
        {
            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                Name = customer.CustomerName,
                PhoneNumber = customer.Phone,
                Email = customer.Email
            };
        }
    }
}