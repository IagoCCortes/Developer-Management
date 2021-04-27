using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.TaskEvents
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