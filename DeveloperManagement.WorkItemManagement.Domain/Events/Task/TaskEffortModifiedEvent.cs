using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class TaskEffortModifiedEvent : DomainEvent
    {
        public byte? OriginalEstimate { get; set; }
        public byte? Remaining { get; set; }
        public byte? Completed { get; set; }

        public TaskEffortModifiedEvent(Effort effort)
        {
            OriginalEstimate = effort.OriginalEstimate;
            Remaining = effort.Remaining;
            Completed = effort.Completed;
        }
    }
}