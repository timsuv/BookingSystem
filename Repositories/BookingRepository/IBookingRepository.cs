using BookingSystem.Models;

namespace BookingSystem.Repositories.BookingRepository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllWithDetails();
        Task<Booking?> GetByIdWithDetails(int id);
        Task<Booking?> GetById(int id);
        Task<Booking> Create(Booking booking);
        Task<Booking> Update(Booking booking);
        Task<bool> Delete(int id);
        Task<IEnumerable<Booking>> GetBookingsByTableAndDate(int tableId, DateTime date, int? excludeBookingId = null);
        Task<IEnumerable<Booking>> GetActiveBookingsForCustomer(int customerId);


    }
}
