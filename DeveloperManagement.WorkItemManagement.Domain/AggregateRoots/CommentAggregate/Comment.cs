using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.CommentAggregate.Events;
using static DeveloperManagement.Core.Domain.Helper.StringHelperMethods;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.CommentAggregate
{
    public class Comment : Entity, IAggregateRoot, IHasDomainEvent
    {
        public string Text { get; private set; }
        public Guid WorkItemId { get; private set; }
        public DateTime Created { get; private set; }
        public string CreatedBy { get; private set; }

        private Comment()
        {
        }

        public Comment(string text, Guid workItemId, DateTime created, string createdBy)
        {
            var invalidFields = AreNullOrWhiteSpace((nameof(Text), text), (nameof(CreatedBy), createdBy));
            if (workItemId == Guid.Empty) invalidFields.Add(nameof(WorkItemId));
            if (invalidFields.Any())
                throw new DomainException(invalidFields
                    .Select(x => new {Key = nameof(x), Value = new[] {$"{nameof(x)} cannot be empty"}})
                    .ToDictionary(x => x.Key, x => x.Value));
            
            Text = text;
            WorkItemId = workItemId;
            Created = created;
            CreatedBy = createdBy;
            
            DomainEvents.Add(new CommentAddedEvent(Id, text, createdBy, created, workItemId));
        }

        public void ModifyComment(string Comment, string user)
        {
            if (user != CreatedBy)
                throw new DomainException(nameof(CreatedBy), "Only the original commenter may modify his comment");

            Text = Comment;
            DomainEvents.Add(new CommentModifiedEvent(Text, Id));
        }

        private List<DomainEvent> _domainEvents;
        public List<DomainEvent> DomainEvents => _domainEvents ??= new List<DomainEvent>();
    }
}