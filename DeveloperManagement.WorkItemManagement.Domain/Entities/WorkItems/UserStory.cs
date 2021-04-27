using System;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.UserStoryEvents;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public sealed class UserStory : WorkItem
    {
        public byte? StoryPoints { get; private set; }
        public Priority? Risk { get; private set; }
        public string AcceptanceCriteria { get; private set; }
        public ValueArea ValueArea { get; private set; }

        private UserStory(string title, Guid area, ValueArea valueArea = ValueArea.Business,
            Priority priority = Priority.Medium) : base(title, area, priority)
        {
            State = WorkItemState.New;
            StateReason = StateReason.New;
            ValueArea = valueArea;
        }

        public override void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            base.ModifyState(state);
        }

        public void ModifyPlanning(Priority priority, byte? storyPoints, Priority? risk)
        {
            Priority = priority;
            StoryPoints = storyPoints;
            Risk = risk;

            DomainEvents.Add(new UserStoryPlanningModifiedEvent(priority, storyPoints, risk));
        }

        public void SpecifyUserStoryInfo(string description, string acceptanceCriteria, ValueArea valueArea)
        {
            Description = description;
            AcceptanceCriteria = acceptanceCriteria;
            ValueArea = valueArea;

            DomainEvents.Add(new UserStoryInfoModifiedEvent(description, acceptanceCriteria, valueArea));
        }

        private StateReason SetStateReason(WorkItemState state)
            => StateReason = state switch
            {
                WorkItemState.New => StateReason.New,
                WorkItemState.Active => StateReason.ImplementationStarted,
                WorkItemState.Resolved => StateReason.CodeCompleteAndUnitTestsPass,
                WorkItemState.Closed => StateReason.AcceptanceTestsPass,
                WorkItemState.Removed => StateReason.RemovedFromTheBacklog,
            };

        public class UserStoryBuilder : WorkItemBuilder<UserStory>
        {
            public UserStoryBuilder(string title, Guid area, ValueArea valueArea = ValueArea.Business,
                Priority priority = Priority.Medium) => WorkItem = new UserStory(title, area, valueArea, priority);

            public UserStoryBuilder SetUserStoryOptionalFields(byte? storyPoints, Priority? risk, string acceptanceCriteria)
            {
                WorkItem.StoryPoints = storyPoints;
                WorkItem.Risk = risk;
                WorkItem.AcceptanceCriteria = acceptanceCriteria;
                return this;
            }

            public override UserStory BuildWorkItem()
            {
                WorkItem.DomainEvents.Add(new UserStoryCreatedEvent(WorkItem));
                return WorkItem;
            }
        }
    }
}