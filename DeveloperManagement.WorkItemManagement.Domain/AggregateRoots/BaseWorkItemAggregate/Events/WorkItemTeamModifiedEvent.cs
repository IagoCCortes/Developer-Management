using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate.Events
{
    public class WorkItemTeamModifiedEvent : DomainEvent
    {
        public Guid TeamId { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemTeamModifiedEvent(Guid teamId, string workItemType)
        {
            TeamId = teamId;
            WorkItemType = workItemType;
        }
    }
}