﻿using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class FeatureWorkItemCreatedEvent : DomainEvent
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

        public FeatureWorkItemCreatedEvent(Feature feature)
        {
            Title = feature.Title;
            State = feature.State;
            Area = feature.Area;
            Iteration = feature.Iteration;
            Priority = feature.Priority;
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