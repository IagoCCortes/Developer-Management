using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.IssueAggregate.Events
{
    public class IssueCreatedEvent : DomainEvent
    {
        public int? StackRank { get; }
        public DateTime? DueDate { get; }
        
        public IssueCreatedEvent(Issue issue)
        {
            StackRank = issue.StackRank;
            DueDate = issue.DueDate;
        }
    }
}