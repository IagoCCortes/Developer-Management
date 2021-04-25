using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    public abstract class DatabaseEntity
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        private List<DomainEvent> _domainEvents;

        protected DatabaseEntity()
        {
            _domainEvents = new List<DomainEvent>();
        }

        protected DatabaseEntity(Entity entity) : this()
        {
            if (entity is IHasDomainEvent hasDomainEvents)
                _domainEvents.AddRange(hasDomainEvents.DomainEvents);
        }

        public bool HasDomainEvents() => _domainEvents.Any();
        public List<DomainEvent> DomainEvents() => _domainEvents;
    }
}