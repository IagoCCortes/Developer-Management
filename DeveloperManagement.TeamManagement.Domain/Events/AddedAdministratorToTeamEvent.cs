using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.TeamManagement.Domain.Events
{
    public class AddedAdministratorToTeamEvent : DomainEvent
    {
        public Guid MemberId { get; }

        public AddedAdministratorToTeamEvent(Guid memberId)
        {
            MemberId = memberId;
        }
    }
}