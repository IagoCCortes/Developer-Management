using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.Development.Domain.ValueObjects
{
    public class Effort : ValueObject
    {
        public byte OriginalEstimate { get; }
        public byte Remaining { get; }
        public byte Completed { get; }

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