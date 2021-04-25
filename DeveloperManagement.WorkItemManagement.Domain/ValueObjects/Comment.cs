using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Comment : ValueObject
    {
        public string Text { get; private set; }
        public DateTime CommentedAt { get; private set; }

        public Comment(string text, DateTime commentedAt)
        {
            Text = text;
            CommentedAt = commentedAt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return CommentedAt;
        }
    }
}