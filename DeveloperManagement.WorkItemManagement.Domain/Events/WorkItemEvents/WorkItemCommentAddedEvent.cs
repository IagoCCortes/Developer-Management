using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItemEvents
{
    public class WorkItemCommentAddedEvent : DomainEvent
    {
        public string Text { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemCommentAddedEvent(string comment, string workItemType)
        {
            Text = comment;
            WorkItemType = workItemType;
        }
    }
}