using BookingSystem.DTOs;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;
        public BookingsController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookings();
            if (bookings == null)
            {
                return NoContent();
            }
            return Ok(bookings);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);

        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var success = await _bookingService.DeleteBooking(id);
            if (!success)
                return NotFound();

            return NoContent();

        }

        [HttpPost("available")]
        public async Task<IActionResult> GetAvailableTables(AvailableTableRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var availableTables = await _bookingService.GetAvailableTables(request);
            return Ok(availableTables);
        }

    }
}
