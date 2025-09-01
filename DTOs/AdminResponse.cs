namespace BookingSystem.DTOs
{
    public class AdminResponse
    {
        public int AdminId { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
