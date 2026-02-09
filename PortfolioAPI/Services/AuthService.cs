using Microsoft.EntityFrameworkCore;
using PortfolioAPI.DTOs;
using PortfolioAPI.Models;
using PortfolioAPI.Repositories.Interfaces;

namespace PortfolioAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<User?> GetUserByUsernameAsync(string username);
    }
    
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        
        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        
        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var users = await _unitOfWork.Users.FindAsync(u => u.Username == loginDto.Username);
            var user = users.FirstOrDefault();
            
            if (user == null)
                return null;
            
            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;
            
            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            
            // Generate JWT token
            var token = _jwtService.GenerateToken(user);
            
            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }
        
        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            // Check if user already exists
            var existingUsers = await _unitOfWork.Users.FindAsync(
                u => u.Username == registerDto.Username || u.Email == registerDto.Email
            );
            
            if (existingUsers.Any())
                return null;
            
            // Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            
            // Create new user
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };
            
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            
            // Generate JWT token
            var token = _jwtService.GenerateToken(user);
            
            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }
        
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var users = await _unitOfWork.Users.FindAsync(u => u.Username == username);
            return users.FirstOrDefault();
        }
    }
}
