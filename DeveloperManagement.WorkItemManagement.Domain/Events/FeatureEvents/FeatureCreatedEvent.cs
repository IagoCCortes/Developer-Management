using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.FeatureEvents
{
    public class FeatureCreatedEvent : DomainEvent
    {
        public Planning Planning { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public FeatureCreatedEvent(Entities.WorkItems.Feature feature)
        {
            Planning = feature.Planning;
            ValueArea = feature.ValueArea;
        }
    }
}