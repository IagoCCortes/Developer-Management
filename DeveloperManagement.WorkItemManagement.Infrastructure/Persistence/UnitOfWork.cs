using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories;
using EventBus;
using Task = System.Threading.Tasks.Task;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDapperConnectionFactory _connectionFactory;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;

        private readonly IDateTime _dateTime;
        private readonly List<IntegrationEventLogEntry> _integrationEvents;
        private readonly List<DatabaseOperationData> _changes;

        private IBugRepository _bugRepository;
        private ITaskRepository _taskRepository;
        private IIntegrationEventRepository _integrationEventRepository;
        
        public IBugRepository BugRepository => _bugRepository ??= new BugRepository(_connectionFactory, _changes);
        public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_changes);

        public IIntegrationEventRepository IntegrationEventRepository =>
            _integrationEventRepository ??= new IntegrationEventRepository(_integrationEvents);

        public UnitOfWork(IDapperConnectionFactory connectionFactory,
            IDomainEventService domainEventService,
            ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _connectionFactory = connectionFactory;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _integrationEvents = new List<IntegrationEventLogEntry>();
            _changes = new List<DatabaseOperationData>();
        }

        public async Task<Guid> SaveChangesAsync()
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await DispatchDomainEvents();

            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            var affectedRows = 0;

            var transactionId = Guid.NewGuid();

            foreach (var change in _changes.Where(c => !c.Sent))
            {
                change.Sent = true;
                switch (change.OperationType)
                {
                    case OperationType.INSERT:
                        change.DbEntity.Created = _dateTime.UtcNow;
                        change.DbEntity.CreatedBy = _currentUserService.UserId;
                        break;
                    case OperationType.UPDATE:
                        change.DbEntity.LastModified = _dateTime.UtcNow;
                        change.DbEntity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }

                affectedRows += await connection.ExecuteAsync(change.Sql, change.DbEntity, transaction);
            }

            foreach (var entry in _integrationEvents)
            {
                entry.SetTransactionId(transactionId);
                affectedRows += await connection.ExecuteAsync(entry.BuildIntegrationEventLogEntryInsertStatement(),
                    entry, transaction);
            }

            transaction.Commit();

            return transactionId;
        }

        private async Task DispatchDomainEvents()
        {
            var domainEvents = _changes.Where(c => c.DbEntity.HasDomainEvents())
                .SelectMany(c => c.DbEntity.DomainEvents());

            foreach (var domainEvent in domainEvents)
                await _domainEventService.Publish(domainEvent);
        }
    }
}