using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public abstract class GenericWriteRepository<T> : IGenericWriteRepository<T> where T : IAggregateRoot
    {
        protected readonly List<(string sql, DatabaseEntity dbEntity, OperationType operationType)> Changes;

        protected GenericWriteRepository(List<(string sql, DatabaseEntity dbEntity, OperationType operationType)> changes)
        {
            Changes = changes;
        }
        
        public abstract void Insert(T aggregateRoot);

        public abstract void Delete(Guid id);
    }
}