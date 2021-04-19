using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemClosedEvent : DomainEvent
    {
        public Guid WorkItemId { get; }
        public IEnumerable<Guid> ChildrenWorkItems { get; }

        public WorkItemClosedEvent(Guid workItemId, IEnumerable<Guid> childrenWorkItems)
        {
            WorkItemId = workItemId;
            ChildrenWorkItems = childrenWorkItems;
        }
    }
}