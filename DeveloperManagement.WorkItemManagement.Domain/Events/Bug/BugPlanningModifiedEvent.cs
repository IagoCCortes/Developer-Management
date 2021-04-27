using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.Bug
{
    public class BugPlanningModifiedEvent : DomainEvent
    {
        public Priority Priority { get; set; }
        public byte? StoryPoints { get; set; }
        public Priority Severity { get; set; }
        public Activity? Activity { get; set; }

        public BugPlanningModifiedEvent(Priority priority, byte? storyPoints, Priority severity, Activity? activity)
        {
            Priority = priority;
            StoryPoints = storyPoints;
            Severity = severity;
            Activity = activity;
        }
    }
}