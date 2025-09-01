using System.ComponentModel.DataAnnotations;

namespace BookingSystem.DTOs
{
    public class CreateTableRequest
    {
        [Required]
        public int TableNumber { get; set; }
        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }
    }
}
