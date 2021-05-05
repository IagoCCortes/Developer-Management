﻿using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public class Period : ValueObject
    {
        public DateTime InitialDateTime { get; private set; }
        public DateTime FinalDateTime { get; private set; }

        private Period() {}
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