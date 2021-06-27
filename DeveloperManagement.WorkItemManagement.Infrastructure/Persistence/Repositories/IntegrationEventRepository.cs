using System.Collections.Generic;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using EventBus;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public class IntegrationEventRepository : IIntegrationEventRepository
    {
        private readonly List<IntegrationEventLogEntry> _integrationEvents;

        public IntegrationEventRepository(List<IntegrationEventLogEntry> integrationEvents)
        {
            _integrationEvents = integrationEvents;
        }

        public void Insert(IntegrationEventLogEntry logEntry)
        {
            _integrationEvents.Add(logEntry);
        }
    }
}