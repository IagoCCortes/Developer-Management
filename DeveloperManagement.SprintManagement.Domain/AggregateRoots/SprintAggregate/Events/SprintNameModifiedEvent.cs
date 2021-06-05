using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Events
{
    public class SprintNameModifiedEvent : DomainEvent
    {
        public Guid SprintId { get; set; }
        public string Name { get; set; }

        public SprintNameModifiedEvent(Guid sprintId, string name)
        {
            SprintId = sprintId;
            Name = name;
        }
    }
}