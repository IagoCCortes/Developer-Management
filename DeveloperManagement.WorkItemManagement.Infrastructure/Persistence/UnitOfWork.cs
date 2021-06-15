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
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories;
using EventBus;
using IntegrationEventLogDapper;
using Task = System.Threading.Tasks.Task;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDapperConnectionFactory _connectionFactory;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;

        public Guid Id { get; set; }

        private readonly IDateTime _dateTime;
        private readonly List<DatabaseOperationData> _changes;
        private readonly List<IntegrationEventLogEntry> _integrationEvents;

        private BugRepository _bugRepository;
        private TaskRepository _taskRepository;

        public IBugRepository BugRepository => _bugRepository ??= new BugRepository(_connectionFactory, _changes);
        public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_changes);

        public UnitOfWork(IDapperConnectionFactory connectionFactory,
            IDomainEventService domainEventService,
            ICurrentUserService currentUserService, IDateTime dateTime)
        {
            Id = Guid.NewGuid();
            _connectionFactory = connectionFactory;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _changes = new List<DatabaseOperationData>();
            _integrationEvents = new List<IntegrationEventLogEntry>();
        }

        public void AddIntegrationEventLogEntry(IntegrationEventLogEntry logEntry) => _integrationEvents.Add(logEntry);

        public async Task<int> SaveChangesAsync()
        {
            await DispatchDomainEvents();

            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            var affectedRows = 0;

            var transactionId = Guid.NewGuid();

            foreach (var change in _changes)
            {
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

            return affectedRows;
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