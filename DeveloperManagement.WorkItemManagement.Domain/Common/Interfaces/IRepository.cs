using System;
using System.Threading.Tasks;
using DeveloperManagement.Core.Domain.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<bool> AggregateExists<T>(Guid aggregateId);
        IDomainUnitOfWork DomainUnitOfWork { get; }
    }
}