using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.FeatureAggregate.Events
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