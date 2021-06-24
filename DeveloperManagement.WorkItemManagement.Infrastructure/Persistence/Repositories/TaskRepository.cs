using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;
using Task = DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate.Task;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private List<DatabaseOperationData> _changes;
        
        public TaskRepository(List<DatabaseOperationData> changes)
        {
            _changes = changes;
        }

        public Task<Task> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Task bug)
        {
            throw new NotImplementedException();
        }
    }
}