using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate.Events
{
    public class TaskCreatedEvent : DomainEvent
    {
        public Activity? Activity { get; set; }
        public Effort Effort { get; set; }
        public string IntegratedInBuild { get; set; }

        public TaskCreatedEvent(Task task)
        {
            Activity = task.Activity;
            Effort = task.Effort;
            IntegratedInBuild = task.IntegratedInBuild;
        }
    }
}