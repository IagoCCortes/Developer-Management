using System.Collections.Generic;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Helper;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Link : ValueObject
    {
        public string Hyperlink { get; private set; }

        public Link(string hyperlink)
        {
            if (hyperlink.IsStringAUrl())
                throw new DomainException(nameof(Hyperlink), "Invalid Url provided for Link");
            Hyperlink = hyperlink;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hyperlink;
        }
    }
}