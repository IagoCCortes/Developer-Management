using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Events
{
    public class SprintFieldModifiedEvent<T> : DomainEvent
    {
        public string Field { get; }
        public T Value { get; }

        public SprintFieldModifiedEvent(string field, T value)
        {
            Field = field;
            Value = value;
        }
    }
}