using PortfolioAPI.Data;
using PortfolioAPI.Models;
using PortfolioAPI.Repositories.Interfaces;

namespace PortfolioAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortfolioDbContext _context;
        
        public IRepository<Project> Projects { get; private set; }
        public IRepository<Skill> Skills { get; private set; }
        public IRepository<Experience> Experiences { get; private set; }
        public IRepository<Education> Education { get; private set; }
        public IRepository<ContactMessage> ContactMessages { get; private set; }
        public IRepository<User> Users { get; private set; }
        
        public UnitOfWork(PortfolioDbContext context)
        {
            _context = context;
            Projects = new Repository<Project>(context);
            Skills = new Repository<Skill>(context);
            Experiences = new Repository<Experience>(context);
            Education = new Repository<Education>(context);
            ContactMessages = new Repository<ContactMessage>(context);
            Users = new Repository<User>(context);
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
