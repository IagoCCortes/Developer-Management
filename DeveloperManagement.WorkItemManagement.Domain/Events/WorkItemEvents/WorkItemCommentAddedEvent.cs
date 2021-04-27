using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItemEvents
{
    public class WorkItemCommentAddedEvent : DomainEvent
    {
        public string Text { get; set; }
        public DateTime CommentedAt { get; set; }
        public string CommentedBy { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemCommentAddedEvent(Comment comment, string workItemType)
        {
            Text = comment.Text;
            CommentedAt = comment.CommentedAt;
            CommentedBy = comment.CommentedBy;
            WorkItemType = workItemType;
        }
    }
}