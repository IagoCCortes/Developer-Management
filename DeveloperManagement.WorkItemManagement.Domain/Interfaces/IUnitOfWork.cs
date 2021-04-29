using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperManagement.WorkItemManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBugRepository BugRepository { get; }
        Task<int> SaveChangesAsync();
    }
}