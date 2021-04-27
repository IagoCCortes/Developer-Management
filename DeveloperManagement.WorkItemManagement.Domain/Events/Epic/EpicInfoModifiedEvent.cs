using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.Epic
{
    public class EpicInfoModifiedEvent : DomainEvent
    {
        public string Description { get; set; }
        public ValueArea ValueArea { get; set; }

        public EpicInfoModifiedEvent(string description, ValueArea valueArea)
        {
            Description = description;
            ValueArea = valueArea;
        }
    }
}