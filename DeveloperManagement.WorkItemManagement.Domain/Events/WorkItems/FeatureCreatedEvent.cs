using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class FeatureCreatedEvent : DomainEvent
    {
        public Planning Planning { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public FeatureCreatedEvent(Feature feature)
        {
            Planning = feature.Planning;
            ValueArea = feature.ValueArea;
        }
    }
}