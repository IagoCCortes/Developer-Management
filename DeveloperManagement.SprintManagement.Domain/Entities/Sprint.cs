using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.SprintManagement.Domain.Enums;
using DeveloperManagement.SprintManagement.Domain.ValueObjects;

namespace DeveloperManagement.SprintManagement.Domain.Entities
{
    public class Sprint : Entity
    {
        public string Name { get; private set; }
        public Period Period { get; private set; }
        public decimal CompletedPercentage { get; private set; }
        public decimal RemainingWorkHours { get; private set; }
        public short ItemsNotEstimated { get; private set; }
        private List<Capacity> _sprintTeam;
        public ReadOnlyCollection<Capacity> SprintTeam => _sprintTeam.AsReadOnly();
        private List<Guid> _userStoryIds;
        public ReadOnlyCollection<Guid> UserStoryIds => _userStoryIds.AsReadOnly();
    }

    public class Capacity : Entity
    {
        public Guid MemberId { get; private set; }
        public Period DaysOff { get; private set; }
        public Activity Activity { get; private set; }
    }
}