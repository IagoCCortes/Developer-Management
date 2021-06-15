using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces
{
    public interface IDomainUnitOfWork
    {
        IBugRepository BugRepository { get; }
        ITaskRepository TaskRepository { get; }
        Task<int> SaveChangesAsync();
    }
}