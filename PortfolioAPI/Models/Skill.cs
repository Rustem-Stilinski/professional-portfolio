using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Skill
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty; // e.g., "Backend", "Frontend", "Database", "Cloud"
        
        public int ProficiencyLevel { get; set; } // 1-5 or 1-100
        
        [MaxLength(500)]
        public string? IconUrl { get; set; }
        
        public int DisplayOrder { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
