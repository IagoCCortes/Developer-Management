using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItemEvents
{
    public class WorkItemAreaModifiedEvent : DomainEvent
    {
        public Guid Area { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemAreaModifiedEvent(Guid area, string workItemType)
        {
            Area = area;
            WorkItemType = workItemType;
        }
    }
}