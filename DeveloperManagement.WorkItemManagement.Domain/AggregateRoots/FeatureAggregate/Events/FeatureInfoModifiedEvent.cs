using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.FeatureAggregate.Events
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