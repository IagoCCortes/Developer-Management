using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public class Capacity : Entity
    {
        public Activity Activity { get; private set; }
        public int CapacityPerDay { get; private set; }
        private readonly List<Period> _daysOff;
        public ReadOnlyCollection<Period> DaysOff => _daysOff.AsReadOnly();

        private Capacity() {}
        public Capacity(Guid id, Activity activity = Activity.Development, int capacityPerDay = 0) : base(id)
        {
            _daysOff = new List<Period>();
            Activity = activity;
            CapacityPerDay = capacityPerDay;
        }

        public void ModifyActivity(Activity activity)
            => Activity = activity;

        public void ModifyCapacityPerDay(int capacity)
            => CapacityPerDay = capacity;

        public void AddDaysOff(Period daysOff)
        {
            if (daysOff == null)
                throw new DomainException(nameof(DaysOff), $"The {nameof(DaysOff)} period must not be empty");

            //End date must be within the current iteration

            _daysOff.Add(daysOff);
        }

        public void RemoveDaysOff(Period daysOff)
        {
            if (daysOff == null)
                throw new DomainException(nameof(DaysOff), $"{nameof(DaysOff)} must not be empty");

            _daysOff.Remove(daysOff);
        }
    }
}