using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemFieldModifiedEvent<T> : DomainEvent
    {
        public string Field { get; set; }
        public T Value { get; set; }

        public WorkItemFieldModifiedEvent(string field, T value)
        {
            Field = field;
            Value = value;
        }
    }
}