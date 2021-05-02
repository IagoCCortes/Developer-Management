using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public class WorkLoad : ValueObject
    {
        public decimal CompletedPercentage { get; private set; }
        public decimal RemainingWorkHours { get; private set; }
        public decimal CompletedWorkHours { get; private set; }
        public int TotalItemsOriginalEstimate { get; private set; }

        private WorkLoad() {}
        public WorkLoad(decimal completedPercentage, decimal remainingWorkHours, decimal completedWorkHours,
            int totalItemsOriginalEstimate)
        {
            CompletedPercentage = completedPercentage;
            RemainingWorkHours = remainingWorkHours;
            CompletedWorkHours = completedWorkHours;
            TotalItemsOriginalEstimate = totalItemsOriginalEstimate;
        }

        public static WorkLoad EmptyWorkLoad() => new WorkLoad(0, 0, 0, 0);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CompletedPercentage;
            yield return RemainingWorkHours;
            yield return CompletedWorkHours;
            yield return TotalItemsOriginalEstimate;
        }
    }
}