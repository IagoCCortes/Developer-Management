using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.SprintManagement.Domain.Events;
using DeveloperManagement.SprintManagement.Domain.ValueObjects;

namespace DeveloperManagement.SprintManagement.Domain.Entities
{
    public class Sprint : Entity, IAggregateRoot, IHasDomainEvent
    {
        public string Name { get; private set; }
        public Period Period { get; private set; }
        public decimal CompletedPercentage { get; private set; }
        public decimal RemainingWorkHours { get; private set; }
        public short ItemsNotEstimated { get; private set; }
        public Guid TeamId { get; }
        private readonly List<Capacity> _capacity;
        public ReadOnlyCollection<Capacity> Capacity => _capacity.AsReadOnly();
        public List<DomainEvent> DomainEvents { get; }

        public Sprint(string name, Period period, Guid teamId)
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
            CompletedPercentage = 0;
            RemainingWorkHours = 0;
            ItemsNotEstimated = 0;

            _capacity = new List<Capacity>();
            DomainEvents = new List<DomainEvent> {new SprintCreatedEvent(this)};
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
                throw new DomainException(nameof(Entities.Capacity), $"{nameof(Entities.Capacity)} must not be empty");

            _capacity.Add(capacity);
            DomainEvents.Add(new AddedCapacityToSprint(capacity));
        }

        public void ModifyCompletedPercentage(decimal newValue)
        {
            CompletedPercentage = newValue;
            DomainEvents.Add(new SprintFieldModifiedEvent<decimal>(nameof(CompletedPercentage), newValue));
        }

        public void ModifyRemainingWorkHours(decimal newValue)
        {
            RemainingWorkHours = newValue;
            DomainEvents.Add(new SprintFieldModifiedEvent<decimal>(nameof(RemainingWorkHours), newValue));
        }

        public void ModifyItemsNotEstimated(short newValue)
        {
            ItemsNotEstimated = newValue;
            DomainEvents.Add(new SprintFieldModifiedEvent<short>(nameof(ItemsNotEstimated), newValue));
        }
    }
}