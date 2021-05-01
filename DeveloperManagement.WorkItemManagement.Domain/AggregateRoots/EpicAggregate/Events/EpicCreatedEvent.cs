using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.EpicAggregate.Events
{
    public class EpicCreatedEvent : DomainEvent
    {
        public Planning Planning { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public EpicCreatedEvent(Epic epic)
        {
            Planning = epic.Planning;
            ValueArea = epic.ValueArea;
        }
    }
}