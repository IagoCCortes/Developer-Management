using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public class WorkItem : Entity
    {
        public Effort Effort { get; private set; }

        private WorkItem() {}
        public WorkItem(Guid id, Effort effort) : base(id)
        {
            Effort = effort ?? throw new DomainException(nameof(Effort), "Effort cannot be null");
        }
    }
}