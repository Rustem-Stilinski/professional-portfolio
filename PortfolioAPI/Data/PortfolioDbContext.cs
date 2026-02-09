using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Project configuration
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Technologies)
                    .HasColumnType("jsonb"); // PostgreSQL JSONB for better querying
                entity.HasIndex(e => e.IsFeatured);
                entity.HasIndex(e => e.DisplayOrder);
            });
            
            // Skill configuration
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Category);
                entity.HasIndex(e => e.DisplayOrder);
            });
            
            // Experience configuration
            modelBuilder.Entity<Experience>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Responsibilities)
                    .HasColumnType("jsonb");
                entity.Property(e => e.Technologies)
                    .HasColumnType("jsonb");
                entity.HasIndex(e => e.DisplayOrder);
            });
            
            // Education configuration
            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Coursework)
                    .HasColumnType("jsonb");
                entity.HasIndex(e => e.DisplayOrder);
            });
            
            // ContactMessage configuration
            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.IsRead);
                entity.HasIndex(e => e.CreatedAt);
            });
            
            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
