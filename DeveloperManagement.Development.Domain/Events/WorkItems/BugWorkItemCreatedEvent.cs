using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Development.Domain.Entities.WorkItems;
using DeveloperManagement.Development.Domain.Enums;
using DeveloperManagement.Development.Domain.ValueObjects;

namespace DeveloperManagement.Development.Domain.Events.WorkItems
{
    public class BugWorkItemCreatedEvent : DomainEvent
    {
        public string Title { get; }
        public WorkItemState State { get; }
        public Guid Area { get; }
        public Guid Iteration { get; }
        public Priority Priority { get; }

        public Effort Effort { get; }
        public string IntegratedInBuild { get; }
        public byte? StoryPoints { get; }
        public Priority Severity { get; }
        public string SystemInfo { get; }
        public string FoundInBuild { get; }

        public BugWorkItemCreatedEvent(Bug bug)
        {
            Title = bug.Title;
            State = bug.State;
            Area = bug.Area;
            Iteration = bug.Iteration;
            Priority = bug.Priority;
            Effort = bug.Effort;
            IntegratedInBuild = bug.IntegratedInBuild;
            StoryPoints = bug.StoryPoints;
            Severity = bug.Severity;
            SystemInfo = bug.SystemInfo;
            FoundInBuild = bug.FoundInBuild;
        }
    }
}