using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class FeatureWorkItemCreatedEvent : DomainEvent
    {
        public byte? Effort { get; }
        public byte? BusinessValue { get; }
        public byte? TimeCriticality { get; }
        public DateTime? StartDate { get; }
        public DateTime? TargetDate { get; }
        public Priority? Risk { get; }
        public ValueArea ValueArea { get; }

        public FeatureWorkItemCreatedEvent(Feature feature)
        {
            Effort = feature.Effort;
            BusinessValue = feature.BusinessValue;
            TimeCriticality = feature.TimeCriticality;
            StartDate = feature.StartDate;
            TargetDate = feature.TargetDate;
            Risk = feature.Risk;
            ValueArea = feature.ValueArea;
        }
    }
}