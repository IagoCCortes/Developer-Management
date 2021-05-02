using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Events
{
    public class AddedCapacityToSprint : DomainEvent
    {
        public Capacity Capacity { get; set; }

        public AddedCapacityToSprint(Capacity capacity)
        {
            Capacity = capacity;
        }
    }
}