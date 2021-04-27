using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class FeaturePlanningModifiedEvent : DomainEvent
    {
        public Priority Priority { get; set; }
        public byte? Effort { get; set; }
        public byte? BusinessValue { get; set; }
        public byte? TimeCriticality { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public Priority? Risk { get; set; }

        public FeaturePlanningModifiedEvent(Planning planning, Priority priority)
        {
            Priority = priority;
            Effort = planning.Effort;
            BusinessValue = planning.BusinessValue;
            TimeCriticality = planning.TimeCriticality;
            StartDate = planning.StartDate;
            TargetDate = planning.TargetDate;
            Risk = planning.Risk;
        }
    }
}