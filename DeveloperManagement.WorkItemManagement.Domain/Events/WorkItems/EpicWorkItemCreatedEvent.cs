using System;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class EpicWorkItemCreatedEvent
    {
        public string Title { get; }
        public WorkItemState State { get; }
        public Guid Area { get; }
        public Guid? Iteration { get; }
        public Priority Priority { get; }

        public byte? Effort { get; }
        public byte? BusinessValue { get; }
        public byte? TimeCriticality { get; }
        public DateTime? StartDate { get; }
        public DateTime? TargetDate { get; }
        public Priority? Risk { get; }
        public ValueArea ValueArea { get; }

        public EpicWorkItemCreatedEvent(Epic epic)
        {
            Title = epic.Title;
            State = epic.State;
            Area = epic.Area;
            Iteration = epic.Iteration;
            Priority = epic.Priority;
            Effort = epic.Effort;
            BusinessValue = epic.BusinessValue;
            TimeCriticality = epic.TimeCriticality;
            StartDate = epic.StartDate;
            TargetDate = epic.TargetDate;
            Risk = epic.Risk;
            ValueArea = epic.ValueArea;
        }
    }
}