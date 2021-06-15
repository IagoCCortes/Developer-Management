using System;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperManagement.SprintManagement.Application.IntegrationEvents.Handlers
{
    public class BugCreatedIntegrationEventHandler : IIntegrationEventHandler<BugCreatedIntegrationEvent>
    {
        private readonly ILogger<BugCreatedIntegrationEventHandler> _logger;

        public BugCreatedIntegrationEventHandler(ILogger<BugCreatedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }
        
        public Task Handle(BugCreatedIntegrationEvent @event)
        {
            Console.WriteLine("Hello World");
            _logger.Log(LogLevel.Critical, @event.ToString());

            return Unit.Task;
        }
    }
}