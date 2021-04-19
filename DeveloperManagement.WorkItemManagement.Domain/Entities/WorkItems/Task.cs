using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Task : WorkItem
    {
        public Activity? Activity { get; private set; }
        public Effort Effort { get; private set; }
        public string IntegratedInBuild { get; private set; }

        private Task()
        {
        }

        public Task(string title, Guid area, Activity activity, Effort effort, string integratedInBuild,
            Priority priority = Priority.Medium) : base(title, area, priority)
        {
            State = WorkItemState.New;
            StateReason = StateReason.New;
            Activity = activity;
            Effort = effort;
            IntegratedInBuild = integratedInBuild;
        }

        public void ModifyState(WorkItemState state, StateReason stateReason)
        {
            ValidateStateAndStateReason(state, stateReason);
            StateReason = stateReason;
            base.ModifyState(state);

            // modify effort, when it is closed remaining hours should be added to completed and then set to 0
        }

        public void ModifyStateReason(StateReason stateReason)
        {
            ValidateStateAndStateReason(State, stateReason);
            StateReason = stateReason;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<StateReason>(nameof(StateReason), stateReason));
        }

        public void ModifyActivity(Activity? activity)
        {
            Activity = activity;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Activity?>(nameof(Activity), activity));
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

        private static void ValidateStateAndStateReason(WorkItemState state, StateReason stateReason)
        {
            if (state == WorkItemState.Resolved)
                throw new DomainException(nameof(State), $"A {nameof(Task)} does not have a {state} state");

            var invalidNew = state == WorkItemState.New && stateReason != StateReason.New;
            var invalidActive = state == WorkItemState.Active && stateReason != StateReason.WorkStarted;
            var invalidClosed = state == WorkItemState.Closed && !stateReason.IsOneOf(StateReason.Completed,
                StateReason.Cut, StateReason.Deferred, StateReason.Obsolete);
            var invalidRemoved = state == WorkItemState.Removed && stateReason != StateReason.RemovedFromTheBacklog;

            if (invalidNew || invalidActive || invalidClosed || invalidRemoved)
                throw new DomainException(nameof(StateReason), "Invalid reason for current state");
        }
    }
}