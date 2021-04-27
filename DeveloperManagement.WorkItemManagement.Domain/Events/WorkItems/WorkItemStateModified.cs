using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemStateModified : DomainEvent
    {
        public WorkItemState State { get; set; }
        public StateReason StateReason { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemStateModified(WorkItemState state, StateReason stateReason, string workItemType)
        {
            State = state;
            StateReason = stateReason;
            WorkItemType = workItemType;
        }
    }
}