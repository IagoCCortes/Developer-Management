using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate.Events
{
    public class WorkItemSprintIdModifiedEvent : DomainEvent
    {
        public Guid? SprintId { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemSprintIdModifiedEvent(Guid? sprintId, string workItemType)
        {
            SprintId = sprintId;
            WorkItemType = workItemType;
        }
    }
}