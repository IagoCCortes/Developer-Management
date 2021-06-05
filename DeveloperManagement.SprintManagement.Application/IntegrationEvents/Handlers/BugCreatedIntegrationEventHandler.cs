using System;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using MediatR;

namespace DeveloperManagement.SprintManagement.Application.IntegrationEvents.Handlers
{
    public class BugCreatedIntegrationEventHandler : IIntegrationEventHandler<BugCreatedIntegrationEvent>
    {
        public Task Handle(BugCreatedIntegrationEvent @event)
        {
            Console.WriteLine("Hello World");

            return Unit.Task;
        }
    }
}