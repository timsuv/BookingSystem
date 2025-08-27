using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        [Range(0, 9999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public bool IsPopular { get; set; } = false;
        [Url]
        public string? BildUrl { get; set; } 

    }
}
