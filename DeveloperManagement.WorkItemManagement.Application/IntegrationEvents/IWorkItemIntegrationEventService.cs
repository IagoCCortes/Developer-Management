using System;
using System.Threading.Tasks;
using EventBus;
using EventBus.Events;

namespace DeveloperManagement.WorkItemManagement.Application.IntegrationEvents
{
    public interface IWorkItemIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        void AddAndSaveEventAsync(IntegrationEvent evt);
    }
}