using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IBugRepository BugRepository { get; }
        Task<int> SaveChangesAsync();
    }
}