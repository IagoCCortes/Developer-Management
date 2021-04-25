using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Issue : WorkItem
    {
        public int? StackRank { get; private set; }
        public DateTime? DueDate { get; private set; }

        public Issue(string title, Guid area, int? stackRank, DateTime? dueDate, Priority priority = Priority.Medium) :
            base(title, area, priority)
        {
            State = WorkItemState.Active;
            StateReason = StateReason.New;
            StackRank = stackRank;
            DueDate = dueDate;
        }

        public override void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            base.ModifyState(state);
        }

        public void ModifyStackRank(int? stackRank)
        {
            StackRank = stackRank;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<int?>(nameof(StackRank), stackRank));
        }

        public void ModifyDueDate(DateTime? dueDate)
        {
            DueDate = dueDate;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<DateTime?>(nameof(DueDate), dueDate));
        }

        private StateReason SetStateReason(WorkItemState state)
            => StateReason = state switch
            {
                WorkItemState.Active => StateReason.New,
                WorkItemState.Closed => StateReason.AddedToBacklog,
                _ => throw new DomainException(nameof(State), $"A {nameof(Issue)} does not have a {state} state")
            };
    }
}