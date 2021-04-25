using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperManagement.WorkItemManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IWorkItemRepository WorkItemRepository { get; }
        Task<int> SaveChangesAsync();
    }
}