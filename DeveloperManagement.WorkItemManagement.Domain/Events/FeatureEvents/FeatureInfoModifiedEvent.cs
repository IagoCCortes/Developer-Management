﻿using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.FeatureEvents
{
    public class FeatureInfoModifiedEvent : DomainEvent
    {
        public string Description { get; set; }
        public ValueArea ValueArea { get; set; }

        public FeatureInfoModifiedEvent(string description, ValueArea valueArea)
        {
            Description = description;
            ValueArea = valueArea;
        }
    }
}