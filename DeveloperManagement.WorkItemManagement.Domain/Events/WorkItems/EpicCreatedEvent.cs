using System;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class EpicCreatedEvent
    {
        public byte? Effort { get; }
        public byte? BusinessValue { get; }
        public byte? TimeCriticality { get; }
        public DateTime? StartDate { get; }
        public DateTime? TargetDate { get; }
        public Priority? Risk { get; }
        public ValueArea ValueArea { get; }

        public EpicCreatedEvent(Epic epic)
        {
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