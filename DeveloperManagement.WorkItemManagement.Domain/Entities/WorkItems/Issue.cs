using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.Issue;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Issue : WorkItem
    {
        public int? StackRank { get; private set; }
        public DateTime? DueDate { get; private set; }

        public Issue(string title, Guid area, Priority priority) : base(title, area, priority)
        {
            State = WorkItemState.Active;
            StateReason = StateReason.New;
        }

        public override void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            base.ModifyState(state);
        }

        public void ModifyPlanning(Priority priority, int? stackRank, DateTime? dueDate)
        {
            Priority = priority;
            StackRank = stackRank;
            DueDate = dueDate;
            
            DomainEvents.Add(new IssuePlanningModifiedEvent(priority, stackRank, dueDate));
        }

        private StateReason SetStateReason(WorkItemState state)
            => StateReason = state switch
            {
                WorkItemState.Active => StateReason.New,
                WorkItemState.Closed => StateReason.AddedToBacklog,
                _ => throw new DomainException(nameof(State), $"A {nameof(Issue)} does not have a {state} state")
            };

        public class IssueBuilder : WorkItemBuilder<Issue>
        {
            public IssueBuilder(string title, Guid area, Priority priority = Priority.Medium) =>
                WorkItem = new Issue(title, area, priority);

            public IssueBuilder SetIssueOptionalFields(int? stackRank, DateTime? dueDate)
            {
                WorkItem.StackRank = stackRank;
                WorkItem.DueDate = dueDate;
                return this;
            }

            public override Issue BuildWorkItem()
            {
                WorkItem.DomainEvents.Add(new IssueCreatedEvent(WorkItem));
                return WorkItem;
            }
        }
    }
}