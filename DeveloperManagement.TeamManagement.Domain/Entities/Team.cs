﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.TeamManagement.Domain.Events;

namespace DeveloperManagement.TeamManagement.Domain.Entities
{
    public class Team : Entity, IAggregateRoot, IHasDomainEvent
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid ActiveSprint { get; private set; }
        public string TeamPicture { get; private set; }
        private List<Guid> _administrators;
        public ReadOnlyCollection<Guid> Administrators => _administrators.AsReadOnly();
        private List<Member> _members;
        public ReadOnlyCollection<Member> Members => _members.AsReadOnly();
        private List<Sprint> _sprints;
        public ReadOnlyCollection<Sprint> Sprints => _sprints.AsReadOnly();
        private List<Guid> _workItems;
        public ReadOnlyCollection<Guid> WorkItems => _workItems.AsReadOnly();
        public List<DomainEvent> DomainEvents { get; }
        
        public Team(string name, string description)
        {
            Name = name;
            Description = description;

            _members = new List<Member>();
            _sprints = new List<Sprint>();
            _workItems = new List<Guid>();
            DomainEvents = new List<DomainEvent> {new CreatedTeamEvent(name, description)};
        }

        public void ModifyName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(nameof(Name), $"{nameof(Name)} must not be empty");

            Name = name;
            DomainEvents.Add(new ModifiedTeamFieldEvent<string>(nameof(Name), name));
        }
        
        public void ModifyDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException(nameof(Description), $"{nameof(Description)} must not be empty");

            Description = description;
            DomainEvents.Add(new ModifiedTeamFieldEvent<string>(nameof(Description), description));
        }
        
        public void ModifyTeamPicture(string teamPicture)
        {
            if (string.IsNullOrWhiteSpace(teamPicture))
                throw new DomainException(nameof(TeamPicture), $"{nameof(TeamPicture)} must not be empty");

            TeamPicture = teamPicture;
            DomainEvents.Add(new ModifiedTeamFieldEvent<string>(nameof(TeamPicture), teamPicture));
        }

        public void AddMember(Member member)
        {
            if (member == null) 
                throw new DomainException(nameof(Members), "A new member must not be empty");

            if (!_members.Contains(member))
            {
                _members.Add(member);
                DomainEvents.Add(new AddedMemberToTeamEvent(member));
            }
        }

        public void AddAdministrator(Member member)
        {
            AddMember(member);

            if (!_administrators.Contains(member.Id))
            {
                _administrators.Add(member.Id);
                DomainEvents.Add(new AddedAdministratorToTeamEvent(member.Id));
            }
        }
    }
}