using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class IssueWorkItemCreatedEvent : DomainEvent
    {
        public int? StackRank { get; }
        public DateTime? DueDate { get; }
        
        public IssueWorkItemCreatedEvent(Issue issue)
        {
            StackRank = issue.StackRank;
            DueDate = issue.DueDate;
        }
    }
}