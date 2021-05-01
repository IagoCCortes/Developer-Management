using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate
{
    public interface IBugRepository : IGenericWriteRepository<Bug>
    {
        Task<Bug> GetByIdAsync(Guid id);
        void ModifyPlanning(Bug bug);
        void SpecifyInfo(Bug bug);
        void ModifyEffort(Bug bug);
    }
}