using System;
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
        public Activity? Activity { get; private set; }
        public string ReproSteps { get; private set; }
        public string SystemInfo { get; private set; }
        public string FoundInBuild { get; private set; }

        private Bug()
        {
        }

        private Bug(string title, Guid area, Priority priority, StateReason stateReason, Priority severity) : base(
            title,
            area, priority)
        {
            ValidateStateAndStateReason(WorkItemState.New, stateReason);
            State = WorkItemState.New;
            StateReason = stateReason;
            Severity = severity;
        }

        public void ModifyPlanning(byte? storyPoints, Priority priority, Priority severity, Activity? activity)
        {
            StoryPoints = storyPoints;
            Priority = priority;
            Severity = severity;
            Activity = activity;
            
            DomainEvents.Add(new BugPlanningModifiedEvent(priority, storyPoints, severity, activity));
        }
        
        public void SpecifyBugInfo(string reproSteps, string systemInfo, string foundInBuild, string integratedInBuild)
        {
            ReproSteps = reproSteps;
            SystemInfo = systemInfo;
            FoundInBuild = foundInBuild;
            IntegratedInBuild = integratedInBuild;
            DomainEvents.Add(new BugInfoModifiedEvent(reproSteps, systemInfo, foundInBuild, integratedInBuild));
        }
        
        public void ModifyEffort(Effort effort)
        {
            Effort = effort;
            DomainEvents.Add(new BugEffortModifiedEvent(effort));
        }

        public void ModifyState(WorkItemState state, StateReason stateReason)
        {
            ValidateStateAndStateReason(state, stateReason);
            StateReason = stateReason;
            base.ModifyState(state);
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

        public class BugBuilder : WorkItemBuilder<Bug>
        {
            public BugBuilder(string title, Guid area, Priority priority = Priority.Medium,
                StateReason stateReason = StateReason.New, Priority severity = Priority.Medium)
            {
                WorkItem = new Bug(title, area, priority, stateReason, severity);
            }

            public BugBuilder SetBugOptionalFields(Effort effort, string integratedInBuild, byte? storyPoints,
                string systemInfo, string foundInBuild)
            {
                WorkItem.Effort = effort;
                WorkItem.IntegratedInBuild = integratedInBuild;
                WorkItem.StoryPoints = storyPoints;
                WorkItem.SystemInfo = systemInfo;
                WorkItem.FoundInBuild = foundInBuild;
                return this;
            }

            public override Bug BuildWorkItem()
            {
                WorkItem.DomainEvents.Add(new BugCreatedEvent(WorkItem));
                return WorkItem;
            }
        }
    }
}