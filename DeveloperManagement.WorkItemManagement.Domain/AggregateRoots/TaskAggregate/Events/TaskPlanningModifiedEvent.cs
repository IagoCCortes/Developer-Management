using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate.Events
{
    public class TaskPlanningModifiedEvent : DomainEvent
    {
        public Priority Priority { get; set; }
        public Activity? Activity { get; set; }

        public TaskPlanningModifiedEvent(Priority priority, Activity? activity)
        {
            Priority = priority;
            Activity = activity;
        }
    }
}