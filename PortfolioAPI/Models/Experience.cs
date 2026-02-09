using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Experience
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Company { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        public string Position { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? Location { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public List<string> Responsibilities { get; set; } = new();
        
        public List<string> Technologies { get; set; } = new();
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsCurrentRole { get; set; }
        
        public int DisplayOrder { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
