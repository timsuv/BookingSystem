using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        [Required]
        public int TableNumber { get; set; }
        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }

        public List<Booking> Bookings { get; set; } = [];
    }
}
