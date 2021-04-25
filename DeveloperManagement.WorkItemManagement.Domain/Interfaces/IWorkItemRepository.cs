using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Interfaces
{
    public interface IWorkItemRepository : IGenericWriteRepository<WorkItem>
    {
    }
}