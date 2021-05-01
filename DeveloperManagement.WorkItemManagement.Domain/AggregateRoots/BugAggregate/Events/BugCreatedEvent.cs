using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate.Events
{
    public class BugCreatedEvent : DomainEvent
    {
        public Effort Effort { get; }
        public string IntegratedInBuild { get; }
        public int? StoryPoints { get; }
        public Priority Severity { get; }
        public string SystemInfo { get; }
        public string FoundInBuild { get; }

        public BugCreatedEvent(Bug bug)
        {
            Effort = bug.Effort;
            IntegratedInBuild = bug.IntegratedInBuild;
            StoryPoints = bug.StoryPoints;
            Severity = bug.Severity;
            SystemInfo = bug.SystemInfo;
            FoundInBuild = bug.FoundInBuild;
        }
    }
}