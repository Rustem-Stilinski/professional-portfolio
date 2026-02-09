using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.DTOs;
using PortfolioAPI.Models;
using PortfolioAPI.Repositories.Interfaces;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProjectsController> _logger;
        
        public ProjectsController(IUnitOfWork unitOfWork, ILogger<ProjectsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll([FromQuery] bool? featuredOnly = null)
        {
            try
            {
                IEnumerable<Project> projects;
                
                if (featuredOnly == true)
                {
                    projects = await _unitOfWork.Projects.FindAsync(p => p.IsFeatured);
                }
                else
                {
                    projects = await _unitOfWork.Projects.GetAllAsync();
                }
                
                return Ok(projects.OrderBy(p => p.DisplayOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving projects");
                return StatusCode(500, new { message = "Error retrieving projects" });
            }
        }
        
        // GET: api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetById(int id)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetByIdAsync(id);
                
                if (project == null)
                    return NotFound(new { message = $"Project with ID {id} not found" });
                
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project {Id}", id);
                return StatusCode(500, new { message = "Error retrieving project" });
            }
        }
        
        // POST: api/projects
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Project>> Create([FromBody] CreateProjectDto dto)
        {
            try
            {
                var project = new Project
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    DetailedDescription = dto.DetailedDescription,
                    ImageUrl = dto.ImageUrl,
                    LiveUrl = dto.LiveUrl,
                    GithubUrl = dto.GithubUrl,
                    Technologies = dto.Technologies,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    IsFeatured = dto.IsFeatured,
                    DisplayOrder = dto.DisplayOrder,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                
                await _unitOfWork.Projects.AddAsync(project);
                await _unitOfWork.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project");
                return StatusCode(500, new { message = "Error creating project" });
            }
        }
        
        // PUT: api/projects/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Project>> Update(int id, [FromBody] UpdateProjectDto dto)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetByIdAsync(id);
                
                if (project == null)
                    return NotFound(new { message = $"Project with ID {id} not found" });
                
                // Update only provided fields
                if (dto.Title != null) project.Title = dto.Title;
                if (dto.Description != null) project.Description = dto.Description;
                if (dto.DetailedDescription != null) project.DetailedDescription = dto.DetailedDescription;
                if (dto.ImageUrl != null) project.ImageUrl = dto.ImageUrl;
                if (dto.LiveUrl != null) project.LiveUrl = dto.LiveUrl;
                if (dto.GithubUrl != null) project.GithubUrl = dto.GithubUrl;
                if (dto.Technologies != null) project.Technologies = dto.Technologies;
                if (dto.StartDate != null) project.StartDate = dto.StartDate.Value;
                if (dto.EndDate != null) project.EndDate = dto.EndDate;
                if (dto.IsFeatured != null) project.IsFeatured = dto.IsFeatured.Value;
                if (dto.DisplayOrder != null) project.DisplayOrder = dto.DisplayOrder.Value;
                
                project.UpdatedAt = DateTime.UtcNow;
                
                await _unitOfWork.Projects.UpdateAsync(project);
                await _unitOfWork.SaveChangesAsync();
                
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project {Id}", id);
                return StatusCode(500, new { message = "Error updating project" });
            }
        }
        
        // DELETE: api/projects/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetByIdAsync(id);
                
                if (project == null)
                    return NotFound(new { message = $"Project with ID {id} not found" });
                
                await _unitOfWork.Projects.DeleteAsync(project);
                await _unitOfWork.SaveChangesAsync();
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project {Id}", id);
                return StatusCode(500, new { message = "Error deleting project" });
            }
        }
    }
}
