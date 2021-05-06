using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using DeveloperManagement.SprintManagement.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Repositories.SprintAR
{
    public class SprintRepository : ISprintRepository
    {
        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public SprintRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Sprint Add(Sprint sprint)
        {
            return _context.Sprints.Add(sprint).Entity;
        }

        public async Task<Sprint> GetAsync(Guid id)
        {
            var sprint = await _context.Sprints
                .Include(x => x.Period)
                .Include(x => x.Capacity)
                .ThenInclude(c => c.Activity)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sprint;
        }

        public void UpdateAddCapacityChangeTracker()
        {
            var activities = _context.ChangeTracker.Entries<Activity>()
                .First(c => c.State != EntityState.Unchanged).State = EntityState.Unchanged;
            
            var capacities = _context.ChangeTracker.Entries<Capacity>()
                .First(c => c.State == EntityState.Modified).State = EntityState.Added;
        }
    }
}