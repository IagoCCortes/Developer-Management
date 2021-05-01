using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects
{
    public class Period : ValueObject
    {
        public DateTime InitialDateTime { get; private set; }
        public DateTime FinalDateTime { get; private set; }

        public Period(DateTime initialDateTime, DateTime finalDateTime)
        {
            InitialDateTime = initialDateTime;
            FinalDateTime = finalDateTime;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InitialDateTime;
            yield return FinalDateTime;
        }
    }
}