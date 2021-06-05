using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Models;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperManagement.WorkItemManagement.Application.DomainEventHandlers
{
    public class BugCreatedEventHandler : INotificationHandler<DomainEventNotification<BugCreatedEvent>>
    {
        private readonly ILogger<BugCreatedEventHandler> _logger;

        public BugCreatedEventHandler(ILogger<BugCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<BugCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}