using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "Admin"; // For future role-based access
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? LastLoginAt { get; set; }
    }
}
