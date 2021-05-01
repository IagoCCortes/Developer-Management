using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.FeatureAggregate.Events
{
    public class FeaturePlanningModifiedEvent : DomainEvent
    {
        public Priority Priority { get; set; }
        public int? Effort { get; set; }
        public int? BusinessValue { get; set; }
        public int? TimeCriticality { get; set; }
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