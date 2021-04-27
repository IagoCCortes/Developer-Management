using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Comment : ValueObject
    {
        public string Text { get; private set; }
        public DateTime CommentedAt { get; private set; }
        public string CommentedBy { get; private set; }

        public Comment(string text, DateTime commentedAt, string commentedBy)
        {
            if (string.IsNullOrWhiteSpace(commentedBy))
                throw new DomainException(nameof(CommentedBy), "Commenter cannot be empty");
            
            Text = text;
            CommentedAt = commentedAt;
            CommentedBy = commentedBy;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return CommentedAt;
            yield return CommentedBy;
        }
    }
}