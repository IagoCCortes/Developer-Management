using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemClosedEvent : DomainEvent
    {
        public Guid WorkItemId { get; }
        public IEnumerable<Guid> ChildrenWorkItems { get; }
        public string WorkItemType { get; }

        public WorkItemClosedEvent(Guid workItemId, IEnumerable<Guid> childrenWorkItems, string workItemType)
        {
            WorkItemId = workItemId;
            ChildrenWorkItems = childrenWorkItems;
            WorkItemType = workItemType;
        }
    }
}