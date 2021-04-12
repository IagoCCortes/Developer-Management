using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Link : ValueObject
    {
        public string Hyperlink { get; }

        public Link(string hyperlink)
        {
            Hyperlink = hyperlink;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hyperlink;
        }
    }
}