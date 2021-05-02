using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Events
{
    public class SprintCreatedEvent : DomainEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; }
        public Period Period { get; }
        public Guid TeamId { get; }
        public decimal CompletedPercentage { get; private set; }
        public decimal RemainingWorkHours { get; private set; }
        public decimal CompletedWorkHours { get; private set; }
        public int TotalItemsOriginalEstimate { get; private set; }
        
        public SprintCreatedEvent(Sprint sprint)
        {
            Id = sprint.Id;
            Name = sprint.Name;
            Period = sprint.Period;
            TeamId = sprint.TeamId;
            CompletedPercentage = sprint.WorkLoad.CompletedPercentage;
            RemainingWorkHours = sprint.WorkLoad.RemainingWorkHours;
            TotalItemsOriginalEstimate = sprint.WorkLoad.TotalItemsOriginalEstimate;
            CompletedWorkHours = sprint.WorkLoad.CompletedWorkHours;
        }
    }
}