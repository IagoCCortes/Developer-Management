using System;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Bug : WorkItem
    {
        public Effort Effort { get; private set; }
        public string IntegratedInBuild { get; private set; }
        public byte? StoryPoints { get; private set; }
        public Priority Severity { get; private set; }
        public string SystemInfo { get; private set; }
        public string FoundInBuild { get; private set; }

        private Bug()
        {
        }

        public Bug(string title, Guid area, StateReason stateReason = StateReason.New) : base(title, area,
            Priority.Medium)
        {
            ValidateStateAndStateReason(WorkItemState.New, stateReason);
            State = WorkItemState.New;
            StateReason = stateReason;
            Severity = Priority.Medium;
        }

        public void ModifyState(WorkItemState state, StateReason stateReason)
        {
            ValidateStateAndStateReason(state, stateReason);
            StateReason = stateReason;
            base.ModifyState(state);
        }

        public void ModifyStateReason(StateReason stateReason)
        {
            ValidateStateAndStateReason(State, stateReason);
            StateReason = stateReason;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<StateReason>(nameof(StateReason), stateReason));
        }

        public void ModifyEffort(Effort effort)
        {
            Effort = effort;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Effort>(nameof(Effort), effort));
        }

        public void ModifyIntegratedInBuild(string integratedInBuild)
        {
            IntegratedInBuild = integratedInBuild;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(IntegratedInBuild), integratedInBuild));
        }

        public void ModifyStoryPoints(byte? points)
        {
            StoryPoints = points;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<byte?>(nameof(StoryPoints), points));
        }

        public void ModifySeverity(Priority severity)
        {
            Severity = severity;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Priority>(nameof(Severity), severity));
        }

        public void ModifyFoundInBuild(string foundInBuild)
        {
            FoundInBuild = foundInBuild;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(FoundInBuild), foundInBuild));
        }

        public void ModifySystemInfo(string systemInfo)
        {
            SystemInfo = systemInfo;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(SystemInfo), systemInfo));
        }

        private void ValidateStateAndStateReason(WorkItemState state, StateReason stateReason)
        {
            if (state == WorkItemState.Removed)
                throw new DomainException(nameof(State), $"A {nameof(Bug)} does not have a {state} state");

            var invalidNew = state == WorkItemState.New &&
                             !stateReason.IsOneOf(StateReason.New, StateReason.BuildFailure);
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