using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.TeamManagement.Domain.Events
{
    public class ModifiedTeamFieldEvent<T> : DomainEvent
    {
        public string Field { get; set; }
        public T Value { get; set; }

        public ModifiedTeamFieldEvent(string field, T value)
        {
            Field = field;
            Value = value;
        }
    }
}