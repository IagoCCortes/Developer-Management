using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.UserStoryAggregate.Events
{
    public class UserStoryCreatedEvent : DomainEvent
    {
        public int? StoryPoints { get; set; }
        public Priority? Risk { get; set; }
        public string AcceptanceCriteria { get; set; }
        public ValueArea ValueArea { get; set; }

        public UserStoryCreatedEvent(UserStory userStory)
        {
            StoryPoints = userStory.StoryPoints;
            Risk = userStory.Risk;
            AcceptanceCriteria = userStory.AcceptanceCriteria;
            ValueArea = userStory.ValueArea;
        }
    }
}