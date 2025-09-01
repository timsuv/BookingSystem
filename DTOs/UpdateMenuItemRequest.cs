using System.ComponentModel.DataAnnotations;

namespace BookingSystem.DTOs
{
    public class UpdateMenuItemRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 9999.99)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsPopular { get; set; } = false;

        [StringLength(255)]
        [Url]
        public string? BildUrl { get; set; }
    }
}
