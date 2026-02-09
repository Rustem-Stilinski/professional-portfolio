using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public string? DetailedDescription { get; set; }
        
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
        
        [MaxLength(500)]
        public string? LiveUrl { get; set; }
        
        [MaxLength(500)]
        public string? GithubUrl { get; set; }
        
        public List<string> Technologies { get; set; } = new();
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsFeatured { get; set; }
        
        public int DisplayOrder { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
