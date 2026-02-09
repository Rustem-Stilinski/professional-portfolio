using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.DTOs;
using PortfolioAPI.Models;
using PortfolioAPI.Repositories.Interfaces;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkillsController> _logger;
        
        public SkillsController(IUnitOfWork unitOfWork, ILogger<SkillsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        // GET: api/skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAll([FromQuery] string? category = null)
        {
            try
            {
                IEnumerable<Skill> skills;
                
                if (!string.IsNullOrEmpty(category))
                {
                    skills = await _unitOfWork.Skills.FindAsync(s => s.Category == category);
                }
                else
                {
                    skills = await _unitOfWork.Skills.GetAllAsync();
                }
                
                return Ok(skills.OrderBy(s => s.DisplayOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving skills");
                return StatusCode(500, new { message = "Error retrieving skills" });
            }
        }
        
        // GET: api/skills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetById(int id)
        {
            try
            {
                var skill = await _unitOfWork.Skills.GetByIdAsync(id);
                
                if (skill == null)
                    return NotFound(new { message = $"Skill with ID {id} not found" });
                
                return Ok(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving skill {Id}", id);
                return StatusCode(500, new { message = "Error retrieving skill" });
            }
        }
        
        // POST: api/skills
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Skill>> Create([FromBody] CreateSkillDto dto)
        {
            try
            {
                var skill = new Skill
                {
                    Name = dto.Name,
                    Category = dto.Category,
                    ProficiencyLevel = dto.ProficiencyLevel,
                    IconUrl = dto.IconUrl,
                    DisplayOrder = dto.DisplayOrder,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                
                await _unitOfWork.Skills.AddAsync(skill);
                await _unitOfWork.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating skill");
                return StatusCode(500, new { message = "Error creating skill" });
            }
        }
        
        // PUT: api/skills/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Skill>> Update(int id, [FromBody] UpdateSkillDto dto)
        {
            try
            {
                var skill = await _unitOfWork.Skills.GetByIdAsync(id);
                
                if (skill == null)
                    return NotFound(new { message = $"Skill with ID {id} not found" });
                
                if (dto.Name != null) skill.Name = dto.Name;
                if (dto.Category != null) skill.Category = dto.Category;
                if (dto.ProficiencyLevel != null) skill.ProficiencyLevel = dto.ProficiencyLevel.Value;
                if (dto.IconUrl != null) skill.IconUrl = dto.IconUrl;
                if (dto.DisplayOrder != null) skill.DisplayOrder = dto.DisplayOrder.Value;
                
                skill.UpdatedAt = DateTime.UtcNow;
                
                await _unitOfWork.Skills.UpdateAsync(skill);
                await _unitOfWork.SaveChangesAsync();
                
                return Ok(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating skill {Id}", id);
                return StatusCode(500, new { message = "Error updating skill" });
            }
        }
        
        // DELETE: api/skills/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var skill = await _unitOfWork.Skills.GetByIdAsync(id);
                
                if (skill == null)
                    return NotFound(new { message = $"Skill with ID {id} not found" });
                
                await _unitOfWork.Skills.DeleteAsync(skill);
                await _unitOfWork.SaveChangesAsync();
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting skill {Id}", id);
                return StatusCode(500, new { message = "Error deleting skill" });
            }
        }
    }
}
