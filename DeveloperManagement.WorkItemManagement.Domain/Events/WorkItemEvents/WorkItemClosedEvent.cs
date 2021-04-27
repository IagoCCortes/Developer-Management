using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItemEvents
{
    public class WorkItemClosedEvent : DomainEvent
    {
        public Guid WorkItemId { get; set; }
        public IEnumerable<Guid> ChildrenWorkItems { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemClosedEvent(Guid workItemId, IEnumerable<Guid> childrenWorkItems, string workItemType)
        {
            WorkItemId = workItemId;
            ChildrenWorkItems = childrenWorkItems;
            WorkItemType = workItemType;
        }
    }
}