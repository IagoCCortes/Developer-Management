using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogDapper;
using Microsoft.Extensions.Logging;

namespace DeveloperManagement.WorkItemManagement.Application.IntegrationEvents
{
    public class WorkItemIntegrationEventService : IWorkItemIntegrationEventService
    {
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly IEventBus _eventBus;
        private ILogger<WorkItemIntegrationEventService> _logger;
        private readonly IUnitOfWork _uow;

        public WorkItemIntegrationEventService(IEventBus eventBus,
            IIntegrationEventLogService eventLogService,
            ILogger<WorkItemIntegrationEventService> logger, IUnitOfWork uow)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow;
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            // cache types
            var eventTypes = Assembly.Load(Assembly.GetExecutingAssembly().FullName)
                .GetTypes()
                .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                .ToList();
            
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId, eventTypes);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation(
                    "----- Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.Id,
                    logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.Id);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId}", logEvt.Id);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.Id);
                }
            }
        }

        public IntegrationEventLogEntry AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation(
                "----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id,
                evt);

            var logEntry = new IntegrationEventLogEntry(evt);
            _uow.AddIntegrationEventLogEntry(logEntry);

            return logEntry;
        }
    }
}