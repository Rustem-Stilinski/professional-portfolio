using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.DTOs;
using PortfolioAPI.Models;
using PortfolioAPI.Repositories.Interfaces;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperiencesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ExperiencesController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experience>>> GetAll()
        {
            var experiences = await _unitOfWork.Experiences.GetAllAsync();
            return Ok(experiences.OrderBy(e => e.DisplayOrder));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Experience>> GetById(int id)
        {
            var experience = await _unitOfWork.Experiences.GetByIdAsync(id);
            return experience == null ? NotFound() : Ok(experience);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Experience>> Create([FromBody] CreateExperienceDto dto)
        {
            var experience = new Experience
            {
                Company = dto.Company,
                Position = dto.Position,
                Location = dto.Location,
                Description = dto.Description,
                Responsibilities = dto.Responsibilities,
                Technologies = dto.Technologies,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrentRole = dto.IsCurrentRole,
                DisplayOrder = dto.DisplayOrder,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            await _unitOfWork.Experiences.AddAsync(experience);
            await _unitOfWork.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = experience.Id }, experience);
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Experience>> Update(int id, [FromBody] UpdateExperienceDto dto)
        {
            var experience = await _unitOfWork.Experiences.GetByIdAsync(id);
            if (experience == null) return NotFound();
            
            if (dto.Company != null) experience.Company = dto.Company;
            if (dto.Position != null) experience.Position = dto.Position;
            if (dto.Location != null) experience.Location = dto.Location;
            if (dto.Description != null) experience.Description = dto.Description;
            if (dto.Responsibilities != null) experience.Responsibilities = dto.Responsibilities;
            if (dto.Technologies != null) experience.Technologies = dto.Technologies;
            if (dto.StartDate != null) experience.StartDate = dto.StartDate.Value;
            if (dto.EndDate != null) experience.EndDate = dto.EndDate;
            if (dto.IsCurrentRole != null) experience.IsCurrentRole = dto.IsCurrentRole.Value;
            if (dto.DisplayOrder != null) experience.DisplayOrder = dto.DisplayOrder.Value;
            
            experience.UpdatedAt = DateTime.UtcNow;
            
            await _unitOfWork.Experiences.UpdateAsync(experience);
            await _unitOfWork.SaveChangesAsync();
            
            return Ok(experience);
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var experience = await _unitOfWork.Experiences.GetByIdAsync(id);
            if (experience == null) return NotFound();
            
            await _unitOfWork.Experiences.DeleteAsync(experience);
            await _unitOfWork.SaveChangesAsync();
            
            return NoContent();
        }
    }
    
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public EducationController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetAll()
        {
            var education = await _unitOfWork.Education.GetAllAsync();
            return Ok(education.OrderBy(e => e.DisplayOrder));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetById(int id)
        {
            var education = await _unitOfWork.Education.GetByIdAsync(id);
            return education == null ? NotFound() : Ok(education);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Education>> Create([FromBody] CreateEducationDto dto)
        {
            var education = new Education
            {
                Institution = dto.Institution,
                Degree = dto.Degree,
                FieldOfStudy = dto.FieldOfStudy,
                Location = dto.Location,
                Description = dto.Description,
                GPA = dto.GPA,
                Coursework = dto.Coursework,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrentlyEnrolled = dto.IsCurrentlyEnrolled,
                DisplayOrder = dto.DisplayOrder,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            await _unitOfWork.Education.AddAsync(education);
            await _unitOfWork.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = education.Id }, education);
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Education>> Update(int id, [FromBody] UpdateEducationDto dto)
        {
            var education = await _unitOfWork.Education.GetByIdAsync(id);
            if (education == null) return NotFound();
            
            if (dto.Institution != null) education.Institution = dto.Institution;
            if (dto.Degree != null) education.Degree = dto.Degree;
            if (dto.FieldOfStudy != null) education.FieldOfStudy = dto.FieldOfStudy;
            if (dto.Location != null) education.Location = dto.Location;
            if (dto.Description != null) education.Description = dto.Description;
            if (dto.GPA != null) education.GPA = dto.GPA;
            if (dto.Coursework != null) education.Coursework = dto.Coursework;
            if (dto.StartDate != null) education.StartDate = dto.StartDate.Value;
            if (dto.EndDate != null) education.EndDate = dto.EndDate;
            if (dto.IsCurrentlyEnrolled != null) education.IsCurrentlyEnrolled = dto.IsCurrentlyEnrolled.Value;
            if (dto.DisplayOrder != null) education.DisplayOrder = dto.DisplayOrder.Value;
            
            education.UpdatedAt = DateTime.UtcNow;
            
            await _unitOfWork.Education.UpdateAsync(education);
            await _unitOfWork.SaveChangesAsync();
            
            return Ok(education);
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var education = await _unitOfWork.Education.GetByIdAsync(id);
            if (education == null) return NotFound();
            
            await _unitOfWork.Education.DeleteAsync(education);
            await _unitOfWork.SaveChangesAsync();
            
            return NoContent();
        }
    }
    
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ContactController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        
        [HttpPost]
        public async Task<ActionResult<ContactMessage>> Submit([FromBody] CreateContactMessageDto dto)
        {
            var message = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Subject = dto.Subject,
                Message = dto.Message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            
            await _unitOfWork.ContactMessages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            
            return Ok(new { message = "Message sent successfully" });
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ContactMessage>>> GetAll([FromQuery] bool? unreadOnly = null)
        {
            IEnumerable<ContactMessage> messages;
            
            if (unreadOnly == true)
            {
                messages = await _unitOfWork.ContactMessages.FindAsync(m => !m.IsRead);
            }
            else
            {
                messages = await _unitOfWork.ContactMessages.GetAllAsync();
            }
            
            return Ok(messages.OrderByDescending(m => m.CreatedAt));
        }
        
        [HttpPut("{id}/read")]
        [Authorize]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _unitOfWork.ContactMessages.GetByIdAsync(id);
            if (message == null) return NotFound();
            
            message.IsRead = true;
            await _unitOfWork.ContactMessages.UpdateAsync(message);
            await _unitOfWork.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
