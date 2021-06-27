using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDomainUnitOfWork
    {
        IIntegrationEventRepository IntegrationEventRepository { get; }
    }
}