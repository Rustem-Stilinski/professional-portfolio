using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<Skill> Skills { get; }
        IRepository<Experience> Experiences { get; }
        IRepository<Education> Education { get; }
        IRepository<ContactMessage> ContactMessages { get; }
        IRepository<User> Users { get; }
        
        Task<int> SaveChangesAsync();
    }
}
