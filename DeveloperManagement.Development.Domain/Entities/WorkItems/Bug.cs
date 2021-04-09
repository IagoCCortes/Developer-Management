using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.Development.Domain.Enums;
using DeveloperManagement.Development.Domain.ValueObjects;

namespace DeveloperManagement.Development.Domain.Entities.WorkItems
{
    public class Bug : WorkItem
    {
        public Effort Effort { get; private set; }
        public string IntegratedInBuild { get; private set; }
        public byte? StoryPoints { get; private set; }
        public Priority Severity { get; private set; }
        public string SystemInfo { get; private set; }
        public string FoundInBuild { get; private set; }

        public Bug(string title, Guid area, Guid iteration, Effort effort,
            string integratedInBuild, byte? storyPoints, string systemInfo,
            string foundInBuild, Priority severity = Priority.Medium, Priority priority = Priority.Medium,
            StateReason stateReason = StateReason.New) : base(title, area, iteration, priority)
        {
            ModifyState(WorkItemState.New, stateReason);

            StateReason = stateReason;
            Effort = effort;
            IntegratedInBuild = integratedInBuild;
            StoryPoints = storyPoints;
            Severity = severity;
            SystemInfo = systemInfo;
            FoundInBuild = foundInBuild;
        }

        public void ModifyState(WorkItemState state, StateReason stateReason)
        {
            ValidateStateAndStateReason(state, stateReason);
            State = state;
            StateReason = stateReason;
        }
        
        public void ModifyStateReason(StateReason stateReason)
        {
            ValidateStateAndStateReason(State, stateReason);
            StateReason = stateReason;
        }
        
        public void ModifyEffort(Effort effort) => Effort = effort;
        
        public void ModifyIntegratedInBuild(string integratedInBuild) => IntegratedInBuild = integratedInBuild;

        public void ModifyStoryPoints(byte? points) => StoryPoints = points;
        
        public void ModifySeverity(Priority severity) => Severity = severity;
        
        public void ModifyFoundInBuild(string foundInBuild) => FoundInBuild = foundInBuild;
        
        public void ModifySystemInfo(string systemInfo) => SystemInfo = systemInfo;
        
        private void ValidateStateAndStateReason(WorkItemState state, StateReason stateReason)
        {
            if (state == WorkItemState.Removed)
                throw new DomainException(nameof(State), $"A {nameof(Bug)} does not have a {state} state");
            
            var invalidNew = state == WorkItemState.New && !stateReason.IsOneOf(StateReason.New, StateReason.BuildFailure);
            var invalidActive = state == WorkItemState.Active &&
                                !stateReason.IsOneOf(StateReason.Approved, StateReason.Investigate);
            var invalidResolved = state == WorkItemState.Resolved && !stateReason.IsOneOf(StateReason.Fixed,
                StateReason.AsDesigned,
                StateReason.CannotReproduce, StateReason.CopiedToBacklog, StateReason.Deferred,
                StateReason.Duplicate, StateReason.Obsolete);
            var invalidClosed = state == WorkItemState.Closed && !stateReason.IsOneOf(StateReason.FixedAndVerified,
                StateReason.AsDesigned,
                StateReason.CannotReproduce, StateReason.CopiedToBacklog, StateReason.Deferred,
                StateReason.Duplicate, StateReason.Obsolete);
            
            if (invalidNew || invalidActive || invalidResolved || invalidClosed)
                throw new DomainException(nameof(StateReason), "Invalid reason for current state");
        }
    }
}