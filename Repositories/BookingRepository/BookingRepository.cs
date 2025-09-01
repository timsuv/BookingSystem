using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories.BookingRepository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RestaurantDbContext _context;
        public BookingRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> GetAllWithDetails()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .OrderBy(b => b.BookingDate)
                .ThenBy(b => b.BookingTime)
                .ToListAsync();
        }
        public async Task<Booking?> GetByIdWithDetails(int id)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.BookingId == id);

        }
        public async Task<Booking?> GetById(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }
        public async Task<Booking> Create(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
        public async Task<Booking> Update(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();  
            return booking;
        }
        public async Task<bool> Delete(int id)
        {
            var booking = await GetById(id);
            if (booking == null)
                return false;
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Booking>> GetBookingsByTableAndDate(int tableId, DateTime date, int? excludeBookingId = null)
        {
            return await _context.Bookings
               .Where(b => b.TableId == tableId &&
                          b.BookingDate.Date == date.Date &&
                          (excludeBookingId == null || b.BookingId != excludeBookingId))
               .ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetActiveBookingsForCustomer(int customerId)
        {
            return await _context.Bookings
                .Where(b => b.CustomerId == customerId && b.BookingDate >= DateTime.Today)
                .ToListAsync();
        }
    }
}
