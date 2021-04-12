using DeveloperManagement.Core.Domain;
using DeveloperManagement.TeamManagement.Domain.Entities;

namespace DeveloperManagement.TeamManagement.Domain.Events
{
    public class AddedMemberToTeamEvent : DomainEvent
    {
        public string Name { get; }
        public string Email { get; }

        public AddedMemberToTeamEvent(Member member)
        {
            Name = member.Name;
            Email = member.EmailAddress.ToString();
        }
    }
}