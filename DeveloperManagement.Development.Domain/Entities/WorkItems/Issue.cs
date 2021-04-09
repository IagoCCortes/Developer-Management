using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Development.Domain.Enums;

namespace DeveloperManagement.Development.Domain.Entities.WorkItems
{
    public class Issue : WorkItem
    {
        public int? StackRank { get; private set; }
        public DateTime? DueDate { get; private set; }

        public Issue(string title, Guid area, Guid iteration, int? stackRank, DateTime? dueDate,
            Priority priority = Priority.Medium) : base(title, area, iteration, priority)
        {
            SetStateReason(WorkItemState.New);
            StackRank = stackRank;
            DueDate = dueDate;
        }

        public void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            State = state;
        }

        public void ModifyStackRank(int? stackRank) => StackRank = stackRank;

        public void ModifyDueDate(DateTime? dueDate) => DueDate = dueDate;

        private void SetStateReason(WorkItemState state)
            => StateReason = state switch
            {
                WorkItemState.Active => StateReason.New,
                WorkItemState.Closed => StateReason.AddedToBacklog,
                _ => throw new DomainException(nameof(State), $"A {nameof(Issue)} does not have a {state} state")
            };
    }
}