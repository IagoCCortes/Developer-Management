using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public class WorkItem : Entity
    {
        public Effort Effort { get; private set; }

        private WorkItem() {}
        public WorkItem(Effort effort)
        {
            if (effort == null)
                throw new DomainException(nameof(Effort), "Effort cannot be null");
            Effort = effort;
        }
    }
}