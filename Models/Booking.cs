using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public TimeSpan BookingTime { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberOfGuests { get; set; }

        // standard bokning är 2 timmar
        public TimeSpan Duration { get; set; } = TimeSpan.FromHours(2);

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TableId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("TableId")]
        public virtual Table Table { get; set; } = null!;

        [NotMapped]
        public TimeSpan EndTime => BookingTime.Add(Duration);
    }
}
