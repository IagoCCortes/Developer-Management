using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.Issue
{
    public class IssueCreatedEvent : DomainEvent
    {
        public int? StackRank { get; }
        public DateTime? DueDate { get; }
        
        public IssueCreatedEvent(Entities.WorkItems.Issue issue)
        {
            StackRank = issue.StackRank;
            DueDate = issue.DueDate;
        }
    }
}