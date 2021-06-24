using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using EventBus;

namespace DeveloperManagement.WorkItemManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDomainUnitOfWork
    {
        void AddIntegrationEventLogEntry(IntegrationEventLogEntry logEntry);
    }
}