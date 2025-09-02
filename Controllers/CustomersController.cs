using BookingSystem.DTOs;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerSevice _customerService;
        public CustomersController(ICustomerSevice customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            if(customers == null) 
                return NotFound("No customer found");
            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if(customer == null)
                return NotFound($"No customer with {id} found");
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerService.CreateCustomer(request);
            
            if(customer == null)
                return BadRequest("Could not create customer");

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerService.UpdateCustomer(id, request);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var success = await _customerService.DeleteCustomer(id);
            if (!success)
                return BadRequest("Cannot delete customer with active bookings");
            return NoContent();
        }
    }
}
