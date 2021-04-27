using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemAReaModifiedEvent : DomainEvent
    {
        public Guid Are { get; set; }
        public string WorkItemType { get; }

        public WorkItemAReaModifiedEvent(Guid are, string workItemType)
        {
            Are = are;
            WorkItemType = workItemType;
        }
    }
}