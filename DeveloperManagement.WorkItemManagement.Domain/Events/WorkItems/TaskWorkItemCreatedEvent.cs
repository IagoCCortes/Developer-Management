using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class TaskWorkItemCreatedEvent : DomainEvent
    {
        public Effort Effort { get; }
        public string IntegratedInBuild { get; }
        public Activity? Activity { get; }

        public TaskWorkItemCreatedEvent(Task task)
        {
            Effort = task.Effort;
            IntegratedInBuild = task.IntegratedInBuild;
            Activity = task.Activity;
        }
    }
}