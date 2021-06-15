using System;
using System.Collections.Generic;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : GenericAggregateRepository<Task>, ITaskRepository
    {
        public TaskRepository(List<DatabaseOperationData> changes) : base(changes)
        {
        }

        public override System.Threading.Tasks.Task<Task> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Task bug)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}