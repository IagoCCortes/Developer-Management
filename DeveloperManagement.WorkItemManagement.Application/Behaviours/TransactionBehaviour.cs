using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Application.IntegrationEvents;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using EventBus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperManagement.WorkItemManagement.Application.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IWorkItemIntegrationEventService _integrationEventService;

        public TransactionBehaviour(IUnitOfWork uow, ILogger<TransactionBehaviour<TRequest, TResponse>> logger,
            IWorkItemIntegrationEventService integrationEventService)
        {
            _uow = uow;
            _logger = logger;
            _integrationEventService = integrationEventService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetGenericTypeName();

            var response = await next();

            var transactionId = await _uow.SaveChangesAsync();

            _logger.LogInformation("----- Transaction {TransactionId} committed for {CommandName}", transactionId,
                typeName);

            await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId);

            return response;
        }
    }
}