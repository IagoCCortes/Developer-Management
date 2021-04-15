using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.SprintManagement.Domain.Entities;
using DeveloperManagement.SprintManagement.Domain.ValueObjects;

namespace DeveloperManagement.SprintManagement.Domain.Events
{
    public class SprintCreatedEvent : DomainEvent
    {
        public string Name { get; }
        public Period Period { get; }
        public Guid TeamId { get; }
        public decimal CompletedPercentage { get; }
        public decimal RemainingWorkHours { get; }
        public short ItemsNotEstimated { get; }
        
        public SprintCreatedEvent(Sprint sprint)
        {
            Name = sprint.Name;
            Period = sprint.Period;
            TeamId = sprint.TeamId;
            CompletedPercentage = sprint.CompletedPercentage;
            RemainingWorkHours = sprint.RemainingWorkHours;
            ItemsNotEstimated = sprint.ItemsNotEstimated;
        }
    }
}