using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.IssueAggregate.Events
{
    public class IssuePlanningModifiedEvent : DomainEvent
    {
        public Priority Priority { get; set; }
        public int? StackRank { get;  set; }
        public DateTime? DueDate { get; set; }

        public IssuePlanningModifiedEvent(Priority priority, int? stackRank, DateTime? dueDate)
        {
            Priority = priority;
            StackRank = stackRank;
            DueDate = dueDate;
        }
    }
}