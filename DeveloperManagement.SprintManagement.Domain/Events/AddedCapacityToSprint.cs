using DeveloperManagement.Core.Domain;
using DeveloperManagement.SprintManagement.Domain.Entities;

namespace DeveloperManagement.SprintManagement.Domain.Events
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