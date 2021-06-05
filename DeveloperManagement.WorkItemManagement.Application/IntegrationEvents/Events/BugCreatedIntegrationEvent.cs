using EventBus.Events;

namespace DeveloperManagement.WorkItemManagement.Application.IntegrationEvents.Events
{
    public class BugCreatedIntegrationEvent : IntegrationEvent
    {
        public int OriginalEstimate { get; }
        public int Remaining { get; }
        public int Completed { get; }

        public BugCreatedIntegrationEvent(int originalEstimate, int remaining, int completed)
        {
            OriginalEstimate = originalEstimate;
            Remaining = remaining;
            Completed = completed;
        }
    }
}