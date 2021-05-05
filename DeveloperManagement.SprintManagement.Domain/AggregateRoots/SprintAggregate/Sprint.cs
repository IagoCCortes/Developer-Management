using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Events;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public class Sprint : Entity, IAggregateRoot, IHasDomainEvent
    {
        public string Name { get; private set; }
        public Period Period { get; private set; }
        public WorkLoad WorkLoad { get; set; }
        public Guid TeamId { get; }
        private readonly List<WorkItem> _workItems;
        public IReadOnlyCollection<WorkItem> WorkItems => _workItems;
        private readonly List<Capacity> _capacity;
        public IReadOnlyCollection<Capacity> Capacity => _capacity;
        private List<DomainEvent> _domainEvents;
        public List<DomainEvent> DomainEvents => _domainEvents ??= new List<DomainEvent>();

        private Sprint()
        {
            _capacity = new List<Capacity>();
            _workItems = new List<WorkItem>();
        }
        
        public Sprint(string name, Period period, Guid teamId) : this()
        {
            var errors = new Dictionary<string, string[]>();
            if (string.IsNullOrWhiteSpace(name))
                errors.Add(nameof(Name), new[] {$"{nameof(Name)} must not be empty"});
            if (period == null)
                errors.Add(nameof(Period), new[] {$"{nameof(Period)} must not be empty"});
            if (teamId == Guid.Empty)
                errors.Add(nameof(TeamId), new[] {$"{nameof(TeamId)} must not be empty"});

            if (errors.Any())
                throw new DomainException(errors);

            Name = name;
            Period = period;
            TeamId = teamId;
            WorkLoad = WorkLoad.EmptyWorkLoad();

            DomainEvents.Add(new SprintCreatedEvent(this));
        }

        public void AddWorkItem(WorkItem workItem)
        {
            if (_workItems.Contains(workItem)) return;

            var originalEstimate = WorkLoad.TotalItemsOriginalEstimate + workItem.Effort.OriginalEstimate;
            var remaining = WorkLoad.RemainingWorkHours + workItem.Effort.Remaining;
            var completed = WorkLoad.CompletedWorkHours + workItem.Effort.Completed;
            var completedPercentage = 100 * completed / (remaining + completed);
            WorkLoad = new WorkLoad(completedPercentage, remaining, completed, originalEstimate);
                

            DomainEvents.Add(new AddedWorkItemToSprint(Id, workItem.Id, workItem.Effort.OriginalEstimate,
                workItem.Effort.Remaining, workItem.Effort.Completed));
        }

        public void ModifySprintName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(nameof(Name), $"{nameof(Name)} must not be empty");

            Name = name;
            DomainEvents.Add(new SprintFieldModifiedEvent<string>(nameof(Name), name));
        }

        public void ModifySprintPeriod(Period period)
        {
            Period = period ??
                     throw new DomainException(nameof(Period), $"{nameof(Period)} must not be empty");
            DomainEvents.Add(new SprintFieldModifiedEvent<Period>(nameof(Period), period));
        }

        public void AddCapacity(Capacity capacity)
        {
            if (capacity == null)
                throw new DomainException(nameof(SprintAggregate.Capacity),
                    $"{nameof(SprintAggregate.Capacity)} must not be empty");

            _capacity.Add(capacity);
            DomainEvents.Add(new AddedCapacityToSprint(capacity));
        }
    }
}