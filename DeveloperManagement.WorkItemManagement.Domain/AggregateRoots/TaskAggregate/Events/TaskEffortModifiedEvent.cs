using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate.Events
{
    public class TaskEffortModifiedEvent : DomainEvent
    {
        public int OriginalEstimate { get; set; }
        public int Remaining { get; set; }
        public int Completed { get; set; }

        public TaskEffortModifiedEvent(Effort effort)
        {
            OriginalEstimate = effort.OriginalEstimate;
            Remaining = effort.Remaining;
            Completed = effort.Completed;
        }
    }
}