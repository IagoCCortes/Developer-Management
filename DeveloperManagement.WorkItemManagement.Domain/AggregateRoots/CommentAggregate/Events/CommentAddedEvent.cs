using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.CommentAggregate.Events
{
    public class CommentAddedEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public string text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public Guid WorkItemId { get; set; }

        public CommentAddedEvent(Guid id, string text, string createdBy, DateTime created, Guid workItemId)
        {
            Id = id;
            this.text = text;
            CreatedBy = createdBy;
            Created = created;
            WorkItemId = workItemId;
        }
    }
}