using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class Administrator
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
