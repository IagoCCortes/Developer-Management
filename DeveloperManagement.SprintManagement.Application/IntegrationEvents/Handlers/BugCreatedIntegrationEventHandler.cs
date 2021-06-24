using System;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Application.Commands.AddWorkItemToSprint;
using DeveloperManagement.SprintManagement.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using EventBus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperManagement.SprintManagement.Application.IntegrationEvents.Handlers
{
    public class BugCreatedIntegrationEventHandler : IIntegrationEventHandler<BugCreatedIntegrationEvent>
    {
        private readonly ILogger<BugCreatedIntegrationEventHandler> _logger;
        private readonly IMediator _mediator;

        public BugCreatedIntegrationEventHandler(ILogger<BugCreatedIntegrationEventHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(BugCreatedIntegrationEvent @event)
        {
            _logger.LogInformation(
                "----- Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})",
                @event.Id, @event);

            var command = new AddWorkItemToSprintCommand
            {
                BugId = @event.BugId,
                Completed = @event.Completed,
                Remaining = @event.Remaining,
                OriginalEstimate = @event.OriginalEstimate,
                SprintId = @event.SprintId
            };
            
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetGenericTypeName(),
                nameof(command.BugId),
                command.BugId,
                command);

            await _mediator.Send(command);
        }
    }
}