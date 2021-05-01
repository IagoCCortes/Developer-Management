using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects
{
    public class Planning : ValueObject
    {
        public int? Effort { get; private set; }
        public int? BusinessValue { get; private set; }
        public int? TimeCriticality { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? TargetDate { get; private set; }
        public Priority? Risk { get; private set; }

        public Planning(int? effort, int? businessValue, int? timeCriticality, DateTime? startDate, DateTime? targetDate, Priority? risk)
        {
            Effort = effort;
            BusinessValue = businessValue;
            TimeCriticality = timeCriticality;
            StartDate = startDate;
            TargetDate = targetDate;
            Risk = risk;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Effort;
            yield return BusinessValue;
            yield return TimeCriticality;
            yield return StartDate;
            yield return TargetDate;
            yield return Risk;
        }
    }
}