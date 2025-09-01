using System.ComponentModel.DataAnnotations;

namespace BookingSystem.DTOs
{
    public class CreateCustomerRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

    }
}
