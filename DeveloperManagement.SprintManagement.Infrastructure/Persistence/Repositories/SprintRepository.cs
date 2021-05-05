using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using DeveloperManagement.SprintManagement.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Repositories
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
            var sprint = await _context.Sprints.Include(x => x.Period)
                .FirstOrDefaultAsync(s => s.Id == id);
            
            if (sprint == null)
            {
                sprint = _context
                    .Sprints
                    .Local
                    .FirstOrDefault(s => s.Id == id);
            }
            // if (sprint != null)
            // {
            //     await _context.Entry(sprint)
            //         .Collection(i => i.Capacity).LoadAsync();
            // }

            return sprint;
        }

        public void Update(Sprint sprint)
        {
            _context.Entry(sprint).State = EntityState.Modified;
        }
    }
}