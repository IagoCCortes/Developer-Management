using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.Development.Domain.Enums;
using DeveloperManagement.Development.Domain.ValueObjects;

namespace DeveloperManagement.Development.Domain.Entities.WorkItems
{
    public class Task : WorkItem
    {
        public Activity? Activity { get; private set; }
        public Effort Effort { get; private set; }
        public string IntegratedInBuild { get; private set; }


        public Task(string title, Guid area, Guid iteration, Activity activity, Effort effort,
            string integratedInBuild, Priority priority = Priority.Medium) : base(title, area, iteration, priority)
        {
            ModifyState(WorkItemState.New, StateReason.New);
            Activity = activity;
            Effort = effort;
            IntegratedInBuild = integratedInBuild;
        }

        public void ModifyState(WorkItemState state, StateReason stateReason)
        {
            ValidateStateAndStateReason(state, stateReason);
            State = state;
            StateReason = stateReason;
            
            // modify effort, when it is closed remaining hours should be added to completed and then set to 0
        }

        public void ModifyStateReason(StateReason stateReason)
        {
            ValidateStateAndStateReason(State, stateReason);
            StateReason = stateReason;
        }

        public void ModifyActivity(Activity? activity) => Activity = activity;

        public void ModifyEffort(Effort effort) => Effort = effort;

        public void ModifyIntegratedInBuild(string integratedInBuild) => IntegratedInBuild = integratedInBuild;

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