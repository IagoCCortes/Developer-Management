﻿using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class BugCreatedEvent : DomainEvent
    {
        public Effort Effort { get; }
        public string IntegratedInBuild { get; }
        public byte? StoryPoints { get; }
        public Priority Severity { get; }
        public string SystemInfo { get; }
        public string FoundInBuild { get; }

        public BugCreatedEvent(Bug bug)
        {
            Effort = bug.Effort;
            IntegratedInBuild = bug.IntegratedInBuild;
            StoryPoints = bug.StoryPoints;
            Severity = bug.Severity;
            SystemInfo = bug.SystemInfo;
            FoundInBuild = bug.FoundInBuild;
        }
    }
}