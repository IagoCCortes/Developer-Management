using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperManagement.SprintManagement.Domain.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}