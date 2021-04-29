using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.TaskEvents;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Task : WorkItem
    {
        public Activity? Activity { get; private set; }
        public Effort Effort { get; private set; }
        public string IntegratedInBuild { get; private set; }

        private Task(string title, Guid area, Priority priority) : base(title, area, priority)
        {
            State = WorkItemState.New;
            StateReason = StateReason.New;
        }

        public void ModifyState(WorkItemState state, StateReason stateReason)
        {
            ValidateStateAndStateReason(state, stateReason);
            StateReason = stateReason;
            
            if (state == WorkItemState.Closed && Effort != null)
                ModifyEffort(new Effort(Effort.OriginalEstimate, 0, Effort.Completed + Effort.Remaining));
            
            base.ModifyState(state);
        }
        
        public void ModifyPlanning(Priority priority, Activity? activity)
        {
            Priority = priority;
            Activity = activity;
            
            DomainEvents.Add(new TaskPlanningModifiedEvent(priority, activity));
        }

        public void ModifyEffort(Effort effort)
        {
            Effort = effort;
            DomainEvents.Add(new TaskEffortModifiedEvent(effort));
        }
        
        public void SpecifyTaskInfo(string description, string integratedInBuild)
        {
            Description = description;
            IntegratedInBuild = integratedInBuild;
            DomainEvents.Add(new TaskInfoModifiedEvent(description, integratedInBuild));
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
        
        public class TaskBuilder : WorkItemBuilder<Task>
        {
            public TaskBuilder(string title, Guid area, Priority priority = Priority.Medium)
            {
                WorkItem = new Task(title, area, priority);
            }

            public TaskBuilder SetTaskOptionalFields(Activity? activity, Effort effort, string integratedInBuild)
            {
                WorkItem.Effort = effort;
                WorkItem.IntegratedInBuild = integratedInBuild;
                WorkItem.Activity = activity;
                return this;
            }

            public override Task BuildWorkItem()
            {
                WorkItem.DomainEvents.Add(new TaskCreatedEvent(WorkItem));
                return WorkItem;
            }
        }
    }
}