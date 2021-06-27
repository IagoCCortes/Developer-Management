using EventBus;

namespace DeveloperManagement.WorkItemManagement.Application.Interfaces
{
    public interface IIntegrationEventRepository
    {
        void Insert(IntegrationEventLogEntry logEntry);
    }
}