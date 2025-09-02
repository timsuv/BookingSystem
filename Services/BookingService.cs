using BookingSystem.Data;
using BookingSystem.DTOs;
using BookingSystem.Models;
using BookingSystem.Repositories.BookingRepository;
using BookingSystem.Repositories.TableRepository;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponse>> GetAllBookings();
        Task<BookingResponse?> GetBookingById(int id);
        Task<BookingResponse?> CreateBooking(CreateBookingRequest request);
        Task<BookingResponse?> UpdateBooking(int id, UpdateBookingRequest request);
        Task<bool> DeleteBooking(int id);
        Task<IEnumerable<AvailableTableResponse>> GetAvailableTables(AvailableTableRequest request);
        Task<bool> IsTableAvailable(int tableId, DateTime date, TimeSpan time, int? excludeBookingId = null);
    }

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITableRepositiory _tableRepository;
        private readonly ICustomerSevice _customerSevice;
        public BookingService(IBookingRepository bookingRepository, ITableRepositiory tableRepository, ICustomerSevice customerSevice)
        {
            _bookingRepository = bookingRepository;
            _tableRepository = tableRepository;
            _customerSevice = customerSevice;
        }

        public async Task<IEnumerable<BookingResponse>> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetAllWithDetails();
            return bookings.Select(MapToResponse);
        }

        public async Task<BookingResponse?> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetByIdWithDetails(id);
            return booking != null ? MapToResponse(booking) : null;
        }


        public async Task<BookingResponse?> CreateBooking(CreateBookingRequest request)
        {
            //först kontrollera att bordet finns
            var table = await _tableRepository.GetById(request.TableId);
            if (table == null)
                return null;
            if (table.Capacity < request.NumberOfGuests)
                return null;

            //sen kontrollera tillgänäglighet
            if (!await IsTableAvailable(request.TableId, request.BookingDate, request.BookingTime))
                return null;

            //hitta/skapa kund
            var customer = await _customerSevice.FindOrCreateCustomer(
                 request.CustomerName, request.CustomerPhone, request.CustomerEmail);

            if (customer == null) return null;

            var booking = new Booking
            {
                BookingDate = request.BookingDate,
                BookingTime = request.BookingTime,
                NumberOfGuests = request.NumberOfGuests,
                CustomerId = customer.CustomerId,
                TableId = request.TableId,
                CreatedAt = DateTime.UtcNow,
            };

            var createdBooking = await _bookingRepository.Create(booking);

            return await GetBookingById(booking.BookingId);

        }

        public async Task<BookingResponse?> UpdateBooking(int id, UpdateBookingRequest request)
        {
            var booking = await _bookingRepository.GetById(id);
            if (booking == null)
                return null;

            //kontroller bordets tillgänglighet 
            var table = await _tableRepository.GetById(id);
            if (table == null || table.Capacity < request.NumberOfGuests)
                return null;

            if (!await IsTableAvailable(request.TableId, request.BookingDate, request.BookingTime, id))
                return null;

            booking.BookingDate = request.BookingDate;
            booking.BookingTime = request.BookingTime;
            booking.NumberOfGuests = request.NumberOfGuests;
            booking.TableId = request.TableId;

            await _bookingRepository.Update(booking);

            return await GetBookingById(id);
        }

        public async Task<bool> DeleteBooking(int id)
        {
            return await _bookingRepository.Delete(id);
        }

        public async Task<IEnumerable<AvailableTableResponse>> GetAvailableTables(AvailableTableRequest request)
        {
            var suitableTables = await _tableRepository.GetTablesByCapacity(request.NumberOfGuests);
            var availableTables = new List<AvailableTableResponse>();
            foreach (var table in suitableTables)
            {
                if (await IsTableAvailable(table.TableId, request.Date, request.Time))
                {
                    availableTables.Add(new AvailableTableResponse
                    {
                        TableId = table.TableId,
                        TableNumber = table.TableNumber,
                        Capacity = table.Capacity
                    });
                }
            }
            return availableTables;
        }


        public async Task<bool> IsTableAvailable(int tableId, DateTime date, TimeSpan time, int? excludeBookingId = null)
        {
            var requestedStart = time;
            var requestedEnd = time.Add(TimeSpan.FromHours(2));

            //hämta alla bokningar för sepcifik datum
            var existingBookings = await _bookingRepository.GetBookingsByTableAndDate(tableId, date, excludeBookingId);


            foreach (var existingsBooking in existingBookings)
            {
                var existingStart = existingsBooking.BookingTime;
                var existingEnd = existingsBooking.EndTime;
                //kontrollera överlappning
                if (requestedStart < existingEnd && requestedEnd > existingStart)
                {
                    return false;
                }
            }
            return true;
        }
        private static BookingResponse MapToResponse(Booking booking)
        {
            return new BookingResponse
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                BookingTime = booking.BookingTime,
                EndTime = booking.EndTime,
                NumberOfGuests = booking.NumberOfGuests,
                CreatedAt = booking.CreatedAt,
                CustomerId = booking.CustomerId,
                CustomerName = booking.Customer.CustomerName,
                CustomerPhone = booking.Customer.Phone,
                CustomerEmail = booking.Customer.Email,
                TableId = booking.TableId,
                TableNumber = booking.Table.TableNumber,
                TableCapacity = booking.Table.Capacity
            };
        }


    }
}
