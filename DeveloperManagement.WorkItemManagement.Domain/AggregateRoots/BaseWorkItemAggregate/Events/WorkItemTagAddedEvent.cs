using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate.Events
{
    public class WorkItemTagAddedEvent : DomainEvent
    {
        public string Tag { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemTagAddedEvent(Tag tag, string workItemType)
        {
            Tag = tag.Text;
            WorkItemType = workItemType;
        }
    }
}