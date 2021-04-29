using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;

namespace DeveloperManagement.WorkItemManagement.Domain.Interfaces
{
    public interface IBugRepository : IGenericWriteRepository<Bug>
    {
        Task<Bug> GetByIdAsync(Guid id);
    }
}