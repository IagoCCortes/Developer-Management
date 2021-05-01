using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.CommentAggregate.Events
{
    public class CommentModifiedEvent : DomainEvent
    {
        public string Text { get; set; }
        public Guid Guid { get; set; }

        public CommentModifiedEvent(string text, Guid guid)
        {
            Text = text;
            Guid = guid;
        }
    }
}