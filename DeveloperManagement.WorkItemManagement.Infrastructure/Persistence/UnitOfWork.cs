using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDapperConnectionFactory _connectionFactory;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly List<(string sql, DatabaseEntity dbEntity, OperationType operationType)> _changes;

        private BugRepository _bugRepository;

        public IBugRepository BugRepository => _bugRepository ??= new BugRepository(_connectionFactory, _changes);

        public UnitOfWork(IDapperConnectionFactory connectionFactory,
            IDomainEventService domainEventService,
            ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _connectionFactory = connectionFactory;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _changes = new List<(string sql, DatabaseEntity dbEntity, OperationType operationType)>();
        }

        public async Task<int> SaveChangesAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();
            var affectedRows = 0;

            foreach (var change in _changes)
            {
                switch (change.operationType)
                {
                    case OperationType.INSERT:
                        change.dbEntity.Created = _dateTime.UtcNow;
                        change.dbEntity.CreatedBy = _currentUserService.UserId;
                        break;
                    case OperationType.UPDATE:
                        change.dbEntity.LastModified = _dateTime.UtcNow;
                        change.dbEntity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }

                affectedRows += await connection.ExecuteAsync(change.sql, change.dbEntity, transaction);
            }

            transaction.Commit();

            await DispatchEvents();

            return affectedRows;
        }

        private async Task DispatchEvents()
        {
            var domainEvents = _changes.Where(c => c.dbEntity.HasDomainEvents())
                .SelectMany(c => c.dbEntity.DomainEvents());

            foreach (var domainEvent in domainEvents)
                await _domainEventService.Publish(domainEvent);
        }
    }
}