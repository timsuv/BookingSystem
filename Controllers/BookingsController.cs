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
        private readonly IBookingService _bookingService;
        public BookingsController(IBookingService bookingService)
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
                return NotFound("No bookings found ");
            }
            return Ok(bookings);
        }   

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
                return NotFound($"Ingen bokning med {id} hittades");

            return Ok(booking);

        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _bookingService.CreateBooking(request);

            if (booking == null)
                return BadRequest("Could not create booking. Check table availability and capacity.");

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var success = await _bookingService.DeleteBooking(id);
            if (!success)
                return NotFound($"Ingen bokning med {id} hittades");

            return NoContent();

        }
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _bookingService.UpdateBooking(id, request);

            if (booking == null)
                return NotFound("Booking not found or could not be updated");

            return Ok(booking);
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
