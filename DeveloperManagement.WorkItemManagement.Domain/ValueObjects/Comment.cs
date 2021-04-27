using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Comment : ValueObject
    {
        public string Text { get; private set; }

        public Comment(string text)
        {
            Text = text;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}