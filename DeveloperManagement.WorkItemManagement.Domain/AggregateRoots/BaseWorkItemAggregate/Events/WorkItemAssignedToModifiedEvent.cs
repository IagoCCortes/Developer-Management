using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate.Events
{
    public class WorkItemAssignedToModifiedEvent : DomainEvent
    {
        public Guid? AssignedTo { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemAssignedToModifiedEvent(Guid? assignedTo, string workItemType)
        {
            AssignedTo = assignedTo;
            WorkItemType = workItemType;
        }
    }
}