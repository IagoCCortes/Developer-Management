using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects
{
    public class Effort : ValueObject
    {
        public int OriginalEstimate { get; private set; }
        public int Remaining { get; private set; }
        public int Completed { get; private set; }
        
        public Effort(int originalEstimate, int remaining, int completed)
        {
            if (originalEstimate < 1)
                throw new DomainException(nameof(OriginalEstimate), "An original estimate must be greater than 0");
                
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