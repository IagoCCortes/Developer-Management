using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate.Events;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate
{
    public class Bug : WorkItem
    {
        public Effort Effort { get; private set; }
        public string IntegratedInBuild { get; private set; }
        public int? StoryPoints { get; private set; }
        public Priority Severity { get; private set; }
        public Activity? Activity { get; private set; }
        public string SystemInfo { get; private set; }
        public string FoundInBuild { get; private set; }

        private Bug()
        {
        }

        private Bug(string title, Guid teamId, Priority priority, StateReason stateReason, Priority severity) : base(
            title,
            teamId, priority)
        {
            ValidateStateAndStateReason(WorkItemState.New, stateReason);
            State = WorkItemState.New;
            StateReason = stateReason;
            Severity = severity;
        }

        public void ModifyPlanning(int? storyPoints, Priority priority, Priority severity, Activity? activity)
        {
            StoryPoints = storyPoints;
            Priority = priority;
            Severity = severity;
            Activity = activity;

            DomainEvents.Add(new BugPlanningModifiedEvent(priority, storyPoints, severity, activity));
        }

        public void SpecifyBugInfo(string description, string systemInfo, string foundInBuild, string integratedInBuild)
        {
            Description = description;
            SystemInfo = systemInfo;
            FoundInBuild = foundInBuild;
            IntegratedInBuild = integratedInBuild;
            DomainEvents.Add(new BugInfoModifiedEvent(description, systemInfo, foundInBuild, integratedInBuild));
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

            if (state == WorkItemState.Closed && Effort != null)
                ModifyEffort(new Effort(Effort.OriginalEstimate, 0, (Effort.Completed + Effort.Remaining)));

            base.ModifyState(state);
        }

        private void ValidateStateAndStateReason(WorkItemState state, StateReason stateReason)
        {
            var invalid = state switch
            {
                WorkItemState.New => stateReason != StateReason.New && StateReason != StateReason.BuildFailure,
                WorkItemState.Active => stateReason != StateReason.Approved && stateReason != StateReason.Investigate,
                WorkItemState.Resolved => stateReason != StateReason.Fixed && StateReason != StateReason.AsDesigned &&
                                          StateReason != StateReason.CannotReproduce &&
                                          StateReason != StateReason.CopiedToBacklog &&
                                          StateReason != StateReason.Deferred && StateReason != StateReason.Duplicate &&
                                          StateReason != StateReason.Obsolete,
                WorkItemState.Closed => stateReason != StateReason.FixedAndVerified &&
                                        StateReason != StateReason.AsDesigned &&
                                        StateReason != StateReason.CannotReproduce &&
                                        StateReason != StateReason.CopiedToBacklog &&
                                        StateReason != StateReason.Deferred && StateReason != StateReason.Duplicate &&
                                        StateReason != StateReason.Obsolete,
                WorkItemState.Removed => throw new DomainException(nameof(State),
                    $"A {nameof(Bug)} does not have a {state} state")
            };

            if (invalid)
                throw new DomainException(nameof(StateReason), "Invalid reason for current state");
        }

        public class BugBuilder : WorkItemBuilder<Bug>
        {
            public BugBuilder(string title, Guid teamId, Priority priority = Priority.Medium,
                StateReason stateReason = StateReason.New, Priority severity = Priority.Medium)
            {
                WorkItem = new Bug(title, teamId, priority, stateReason, severity);
            }

            public BugBuilder SetBugOptionalFields(Effort effort, string integratedInBuild, int? storyPoints,
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