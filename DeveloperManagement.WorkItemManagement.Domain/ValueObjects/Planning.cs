using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Planning : ValueObject
    {
        public byte? Effort { get; private set; }
        public byte? BusinessValue { get; private set; }
        public byte? TimeCriticality { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? TargetDate { get; private set; }
        public Priority? Risk { get; private set; }

        public Planning(byte? effort, byte? businessValue, byte? timeCriticality, DateTime? startDate, DateTime? targetDate, Priority? risk)
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