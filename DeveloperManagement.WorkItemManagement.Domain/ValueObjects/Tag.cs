using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Tag : ValueObject
    {
        public string Text { get; private set; }

        public Tag(string text)
        {
            Text = text;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}