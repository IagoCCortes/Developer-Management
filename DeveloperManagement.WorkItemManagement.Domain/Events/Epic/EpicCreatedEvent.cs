using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.Epic
{
    public class EpicCreatedEvent : DomainEvent
    {
        public Planning Planning { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public EpicCreatedEvent(Entities.WorkItems.Epic epic)
        {
            Planning = epic.Planning;
            ValueArea = epic.ValueArea;
        }
    }
}