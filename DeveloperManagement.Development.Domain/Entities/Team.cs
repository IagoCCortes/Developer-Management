using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.Development.Domain.ValueObjects;

namespace DeveloperManagement.Development.Domain.Entities
{
    public class Team : Entity, IAggregateRoot, IHasDomainEvent
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public short AverageWorkInProgress { get; private set; }
        public Sprint ActiveSprint { get; private set; }
        public Period DaysOf { get; private set; }
        public string ProfilePicture { get; private set; }
        private List<Member> _administrators;

        public ReadOnlyCollection<Member> Administrators => _administrators.AsReadOnly();
        
        private List<Member> _members;
        public ReadOnlyCollection<Member> Members => _members.AsReadOnly();

        private List<Sprint> _sprints;
        
        public ReadOnlyCollection<Sprint> Sprints => _sprints.AsReadOnly();
        public List<DomainEvent> DomainEvents { get; }
        

        public Team(string name, string description)
        {
            Name = name;
            Description = description;

            _members = new List<Member>();
            _sprints = new List<Sprint>();
            DomainEvents = new List<DomainEvent>();
            
            // new domain event to create a backlog and link id
        }

        public void AddMember(Member member)
        {
            if (member == null) 
                throw new DomainException(nameof(Members), "A new member must not be null");
            
            _members.Add(member);
            // domain event email new member
        }

        public void AddAdministrator(Member member)
        {
            if (member == null)
                throw new DomainException(nameof(Members), "An administrator must not be null");
            
            _administrators.Add(member);
            
            if (!_members.Contains(member))
                _members.Add(member);
        }
    }
}