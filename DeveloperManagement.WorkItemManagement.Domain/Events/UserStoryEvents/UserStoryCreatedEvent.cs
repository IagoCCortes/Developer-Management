using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.UserStoryEvents
{
    public class UserStoryCreatedEvent : DomainEvent
    {
        public byte? StoryPoints { get; set; }
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