using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate.Events;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate
{
    public class Task : WorkItem
    {
        public Activity? Activity { get; private set; }
        public Effort Effort { get; private set; }
        public string IntegratedInBuild { get; private set; }

        private Task(string title, Guid teamId, Priority priority, Effort effort) : base(title, teamId, priority)
        {
            if (effort == null) throw new DomainException(nameof(Effort), "Effort cannot be empty");
            State = WorkItemState.New;
            StateReason = StateReason.New;
            Effort = effort;
        }

        public void ModifyState(WorkItemState state, StateReason stateReason)
        {
            ValidateStateAndStateReason(state, stateReason);
            StateReason = stateReason;

            if (state == WorkItemState.Closed && Effort != null)
                ModifyEffort(0, Effort.Completed + Effort.Remaining);

            base.ModifyState(state);
        }

        public void ModifyPlanning(Priority priority, Activity? activity)
        {
            Priority = priority;
            Activity = activity;

            DomainEvents.Add(new TaskPlanningModifiedEvent(priority, activity));
        }

        public void ModifyEffort(int remaining, int completed)
        {
            Effort = new Effort(Effort.OriginalEstimate, remaining, completed);
            DomainEvents.Add(new TaskEffortModifiedEvent(Effort));
        }

        public void SpecifyTaskInfo(string description, string integratedInBuild)
        {
            Description = description;
            IntegratedInBuild = integratedInBuild;
            DomainEvents.Add(new TaskInfoModifiedEvent(description, integratedInBuild));
        }

        private static void ValidateStateAndStateReason(WorkItemState state, StateReason stateReason)
        {
            var invalid = state switch
            {
                WorkItemState.New => stateReason != StateReason.New,
                WorkItemState.Active => stateReason != StateReason.WorkStarted,
                WorkItemState.Closed => stateReason != StateReason.Completed && stateReason != StateReason.Cut &&
                                        stateReason != StateReason.Deferred && stateReason != StateReason.Obsolete,
                WorkItemState.Removed => stateReason != StateReason.RemovedFromTheBacklog,
                WorkItemState.Resolved => throw new DomainException(nameof(State),
                    $"A {nameof(Task)} does not have a {state} state"),
            };

            if (invalid)
                throw new DomainException(nameof(StateReason), "Invalid reason for current state");
        }

        public class TaskBuilder : WorkItemBuilder<Task>
        {
            public TaskBuilder(string title, Guid teamId, Effort effort, Priority priority = Priority.Medium)
            {
                WorkItem = new Task(title, teamId, priority, effort);
            }

            public TaskBuilder SetTaskOptionalFields(Activity? activity, string integratedInBuild)
            {
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