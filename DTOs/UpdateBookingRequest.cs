namespace BookingSystem.DTOs
{
    public class UpdateBookingRequest
    {
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public int NumberOfGuests { get; set; }
        public int TableId { get; set; }
    }
}
