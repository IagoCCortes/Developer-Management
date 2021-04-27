using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItemEvents
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