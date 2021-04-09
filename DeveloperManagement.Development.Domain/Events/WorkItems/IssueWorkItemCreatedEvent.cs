using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Development.Domain.Entities.WorkItems;
using DeveloperManagement.Development.Domain.Enums;

namespace DeveloperManagement.Development.Domain.Events.WorkItems
{
    public class IssueWorkItemCreatedEvent : DomainEvent
    {
        public string Title { get; }
        public WorkItemState State { get; }
        public Guid Area { get; }
        public Guid Iteration { get; }
        public Priority Priority { get; }
        
        public int? StackRank { get; }
        public DateTime? DueDate { get; }
        
        public IssueWorkItemCreatedEvent(Issue issue)
        {
            Title = issue.Title;
            State = issue.State;
            Area = issue.Area;
            Iteration = issue.Iteration;
            Priority = issue.Priority;
            StackRank = issue.StackRank;
            DueDate = issue.DueDate;
        }
    }
}