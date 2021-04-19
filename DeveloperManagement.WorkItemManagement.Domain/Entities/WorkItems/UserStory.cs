using System;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public sealed class UserStory : WorkItem
    {
        public byte? StoryPoints { get; private set; }
        public Priority? Risk { get; private set; }
        public string AcceptanceCriteria { get; private set; }
        public ValueArea ValueArea { get; private set; }

        private UserStory()
        {
        }

        public UserStory(string title, Guid area, byte? storyPoints, Priority? risk, string acceptanceCriteria,
            ValueArea valueArea = ValueArea.Business, Priority priority = Priority.Medium) : base(title, area, priority)
        {
            State = WorkItemState.New;
            StateReason = StateReason.New;
            StoryPoints = storyPoints;
            Risk = risk;
            AcceptanceCriteria = acceptanceCriteria;
            ValueArea = valueArea;
        }

        public override void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            base.ModifyState(state);
        }

        public void ModifyStoryPoints(byte? points)
        {
            StoryPoints = points;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<byte?>(nameof(StoryPoints), points));
        }

        public void ModifyRisk(Priority? risk)
        {
            Risk = risk;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Priority?>(nameof(Risk), risk));
        }

        public void ModifyAcceptanceCriteria(string criteria)
        {
            AcceptanceCriteria = criteria;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(AcceptanceCriteria), criteria));
        }

        public void ModifyValueArea(ValueArea valueArea)
        {
            ValueArea = valueArea;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<ValueArea>(nameof(ValueArea), valueArea));
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
    }
}