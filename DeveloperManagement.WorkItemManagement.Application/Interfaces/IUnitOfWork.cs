using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using EventBus;
using IntegrationEventLogDapper;

namespace DeveloperManagement.WorkItemManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDomainUnitOfWork
    {
        void AddIntegrationEventLogEntry(IntegrationEventLogEntry logEntry);
    }
}