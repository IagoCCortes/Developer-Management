using System;
using DeveloperManagement.Development.Domain.Enums;

namespace DeveloperManagement.Development.Domain.Entities.WorkItems
{
    public class UserStory : WorkItem
    {
        public byte? StoryPoints { get; private set; }
        public Priority? Risk { get; private set; }
        public string AcceptanceCriteria { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public UserStory(string title, Guid area, Guid iteration, byte? storyPoints, Priority? risk,
            string acceptanceCriteria, ValueArea valueArea = ValueArea.Business, Priority priority = Priority.Medium) : base(title, area,
            iteration, priority)
        {
            ModifyState(WorkItemState.New);
            StoryPoints = storyPoints;
            Risk = risk;
            AcceptanceCriteria = acceptanceCriteria;
            ValueArea = valueArea;
        }

        public void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            State = state;
        }

        public void ModifyStoryPoints(byte? points) => StoryPoints = points;

        public void ModifyRisk(Priority? risk) => Risk = risk;

        public void ModifyAccepyanceCriteria(string criteria) => AcceptanceCriteria = criteria;

        public void ModifyValueArea(ValueArea valueArea) => ValueArea = valueArea;

        private void SetStateReason(WorkItemState state)
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