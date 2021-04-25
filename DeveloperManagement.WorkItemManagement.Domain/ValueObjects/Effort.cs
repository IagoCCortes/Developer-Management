using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Effort : ValueObject
    {
        public byte OriginalEstimate { get; private set; }
        public byte Remaining { get; private set; }
        public byte Completed { get; private set; }
        
        public Effort(byte originalEstimate, byte remaining, byte completed)
        {
            OriginalEstimate = originalEstimate;
            Remaining = remaining;
            Completed = completed;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OriginalEstimate;
            yield return Remaining;
            yield return Completed;
        }
    }
}