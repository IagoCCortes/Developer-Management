using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.UserStoryAggregate.Events
{
    public class UserStoryInfoModifiedEvent : DomainEvent
    {
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public ValueArea ValueArea { get; set; }

        public UserStoryInfoModifiedEvent(string description, string acceptanceCriteria, ValueArea valueArea)
        {
            Description = description;
            AcceptanceCriteria = acceptanceCriteria;
            ValueArea = valueArea;
        }
    }
}