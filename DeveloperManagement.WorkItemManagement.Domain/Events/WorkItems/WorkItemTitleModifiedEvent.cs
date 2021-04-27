using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemTitleModifiedEvent : DomainEvent
    {
        public string Title { get; set; }
        public string WorkItemType { get; }

        public WorkItemTitleModifiedEvent(string title, string workItemType)
        {
            Title = title;
            WorkItemType = workItemType;
        }
    }
}