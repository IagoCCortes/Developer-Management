using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemIterationModifiedEvent : DomainEvent
    {
        public Guid? Iteration { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemIterationModifiedEvent(Guid? iteration, string workItemType)
        {
            Iteration = iteration;
            WorkItemType = workItemType;
        }
    }
}