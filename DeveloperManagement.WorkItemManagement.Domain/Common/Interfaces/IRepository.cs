using DeveloperManagement.Core.Domain.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IDomainUnitOfWork DomainUnitOfWork { get; }
    }
}