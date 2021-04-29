using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.BugEvents
{
    public class BugEffortModifiedEvent : DomainEvent
    {
        public int? OriginalEstimate { get; set; }
        public int? Remaining { get; set; }
        public int? Completed { get; set; }

        public BugEffortModifiedEvent(Effort effort)
        {
            OriginalEstimate = effort?.OriginalEstimate;
            Remaining = effort?.Remaining;
            Completed = effort?.Completed;
        }
    }
}