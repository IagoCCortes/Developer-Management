using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate.Events
{
    public class WorkItemRepoLinkModifiedEvent : DomainEvent
    {
        public string Url { get; set; }
        public string WorkItemType { get; }

        public WorkItemRepoLinkModifiedEvent(string url, string workItemType)
        {
            Url = url;
            WorkItemType = workItemType;
        }
    }
}