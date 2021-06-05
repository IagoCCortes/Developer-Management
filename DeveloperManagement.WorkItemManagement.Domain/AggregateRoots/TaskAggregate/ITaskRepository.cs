using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate
{
    public interface ITaskRepository : IGenericWriteRepository<Task>
    {
        Task<Task> GetByIdAsync(Guid id);
    }
}