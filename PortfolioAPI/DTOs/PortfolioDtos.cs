using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.DTOs
{
    // Auth DTOs
    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
    
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
    
    // Project DTOs
    public class CreateProjectDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public string? DetailedDescription { get; set; }
        public string? ImageUrl { get; set; }
        public string? LiveUrl { get; set; }
        public string? GithubUrl { get; set; }
        public List<string> Technologies { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsFeatured { get; set; }
        public int DisplayOrder { get; set; }
    }
    
    public class UpdateProjectDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DetailedDescription { get; set; }
        public string? ImageUrl { get; set; }
        public string? LiveUrl { get; set; }
        public string? GithubUrl { get; set; }
        public List<string>? Technologies { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsFeatured { get; set; }
        public int? DisplayOrder { get; set; }
    }
    
    // Skill DTOs
    public class CreateSkillDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;
        
        [Range(1, 100)]
        public int ProficiencyLevel { get; set; }
        
        public string? IconUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
    
    public class UpdateSkillDto
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        
        [Range(1, 100)]
        public int? ProficiencyLevel { get; set; }
        
        public string? IconUrl { get; set; }
        public int? DisplayOrder { get; set; }
    }
    
    // Experience DTOs
    public class CreateExperienceDto
    {
        [Required]
        public string Company { get; set; } = string.Empty;
        
        [Required]
        public string Position { get; set; } = string.Empty;
        
        public string? Location { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public List<string> Responsibilities { get; set; } = new();
        public List<string> Technologies { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentRole { get; set; }
        public int DisplayOrder { get; set; }
    }
    
    public class UpdateExperienceDto
    {
        public string? Company { get; set; }
        public string? Position { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public List<string>? Responsibilities { get; set; }
        public List<string>? Technologies { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentRole { get; set; }
        public int? DisplayOrder { get; set; }
    }
    
    // Education DTOs
    public class CreateEducationDto
    {
        [Required]
        public string Institution { get; set; } = string.Empty;
        
        [Required]
        public string Degree { get; set; } = string.Empty;
        
        public string? FieldOfStudy { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public double? GPA { get; set; }
        public List<string> Coursework { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentlyEnrolled { get; set; }
        public int DisplayOrder { get; set; }
    }
    
    public class UpdateEducationDto
    {
        public string? Institution { get; set; }
        public string? Degree { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public double? GPA { get; set; }
        public List<string>? Coursework { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyEnrolled { get; set; }
        public int? DisplayOrder { get; set; }
    }
    
    // Contact DTOs
    public class CreateContactMessageDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string? Subject { get; set; }
        
        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
