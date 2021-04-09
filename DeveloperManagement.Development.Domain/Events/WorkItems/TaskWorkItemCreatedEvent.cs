using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Development.Domain.Entities.WorkItems;
using DeveloperManagement.Development.Domain.Enums;
using DeveloperManagement.Development.Domain.ValueObjects;

namespace DeveloperManagement.Development.Domain.Events.WorkItems
{
    public class TaskWorkItemCreatedEvent : DomainEvent
    {
        public string Title { get; }
        public WorkItemState State { get; }
        public Guid Area { get; }
        public Guid Iteration { get; }
        public Priority Priority { get; }

        public Effort Effort { get; }
        public string IntegratedInBuild { get; }
        public Activity? Activity { get; }

        public TaskWorkItemCreatedEvent(Task task)
        {
            Title = task.Title;
            State = task.State;
            Area = task.Area;
            Iteration = task.Iteration;
            Priority = task.Priority;
            Effort = task.Effort;
            IntegratedInBuild = task.IntegratedInBuild;
            Activity = task.Activity;
        }
    }
}