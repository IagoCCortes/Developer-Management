using System;
using EventBus.Events;

namespace DeveloperManagement.WorkItemManagement.Application.IntegrationEvents.Events
{
    public class BugCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid BugId { get; }
        public Guid SprintId { get; }
        public int OriginalEstimate { get; }
        public int Remaining { get; }
        public int Completed { get; }

        public BugCreatedIntegrationEvent(Guid bugId, int originalEstimate, int remaining, int completed, Guid sprintId)
        {
            BugId = bugId;
            OriginalEstimate = originalEstimate;
            Remaining = remaining;
            Completed = completed;
            SprintId = sprintId;
        }
    }
}