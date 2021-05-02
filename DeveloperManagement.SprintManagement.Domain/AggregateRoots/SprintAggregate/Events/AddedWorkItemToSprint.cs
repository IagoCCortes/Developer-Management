using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Events
{
    public class AddedWorkItemToSprint : DomainEvent
    {
        public Guid SprintId { get; private set; }
        public Guid WorkItemId { get; private set; }
        public int OriginalEstimate { get; private set; }
        public int Remaining { get; private set; }
        public int Completed { get; private set; }

        public AddedWorkItemToSprint(Guid sprintId, Guid workItemId, int originalEstimate, int remaining, int completed)
        {
            SprintId = sprintId;
            WorkItemId = workItemId;
            OriginalEstimate = originalEstimate;
            Remaining = remaining;
            Completed = completed;
        }
    }
}