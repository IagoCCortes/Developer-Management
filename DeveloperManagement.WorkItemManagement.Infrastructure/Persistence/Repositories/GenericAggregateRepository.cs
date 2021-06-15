using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public abstract class GenericAggregateRepository<T> : IGenericWriteRepository<T> where T : IAggregateRoot
    {
        protected readonly List<DatabaseOperationData> Changes;

        protected GenericAggregateRepository(List<DatabaseOperationData> changes)
        {
            Changes = changes;
        }

        public abstract Task<T> GetByIdAsync(Guid id);
        
        public abstract void Insert(T bug);

        public abstract void Delete(Guid id);
    }
}