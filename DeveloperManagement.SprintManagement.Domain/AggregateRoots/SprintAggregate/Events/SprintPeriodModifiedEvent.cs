using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Events
{
    public class SprintPeriodModifiedEvent : DomainEvent
    {
        public Guid SprintId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        public SprintPeriodModifiedEvent(Guid sprintId, DateTime initialDate, DateTime finalDate)
        {
            SprintId = sprintId;
            InitialDate = initialDate;
            FinalDate = finalDate;
        }
    }
}