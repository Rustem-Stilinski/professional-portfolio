using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Education
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Institution { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        public string Degree { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? FieldOfStudy { get; set; }
        
        [MaxLength(100)]
        public string? Location { get; set; }
        
        public string? Description { get; set; }
        
        public double? GPA { get; set; }
        
        public List<string> Coursework { get; set; } = new();
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsCurrentlyEnrolled { get; set; }
        
        public int DisplayOrder { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
