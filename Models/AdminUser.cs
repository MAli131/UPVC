namespace UPVC.Models
{
    public class AdminUser : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
