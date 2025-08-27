using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? CustomerName { get; set; }

        [Phone]
        [Required]
        [MinLength(9)] // Utan landskod
        [MaxLength(15)] // Med landskod
        public string? Phone { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        public virtual List<Booking> Bookings {get; set;}  = [];

    }
}
