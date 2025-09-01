using System.ComponentModel.DataAnnotations;

namespace BookingSystem.DTOs
{
    public class AvailableTableRequest
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        [Required]
        [Range(1, 20)]
        public int NumberOfGuests { get; set; }
    }
}
