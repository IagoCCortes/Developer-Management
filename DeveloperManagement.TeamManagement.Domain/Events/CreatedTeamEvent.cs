using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.TeamManagement.Domain.Events
{
    public class CreatedTeamEvent : DomainEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreatedTeamEvent(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}