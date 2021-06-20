using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.Services
{
    public interface IWorkItemLookup
    {
        Task<bool> WorkItemExists(Guid workItemId);
    }

    public class WorkItemLookup : IWorkItemLookup
    {
        private IDomainUnitOfWork _uow;

        public WorkItemLookup(IDomainUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public Task<bool> WorkItemExists(Guid workItemId)
        {
            throw new NotImplementedException();
        }
    }
}