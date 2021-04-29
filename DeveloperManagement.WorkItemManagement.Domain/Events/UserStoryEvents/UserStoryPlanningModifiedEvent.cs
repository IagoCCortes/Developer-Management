﻿using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.UserStoryEvents
{
    public class UserStoryPlanningModifiedEvent : DomainEvent
    {
        public Priority Priority { get; set; }
        public int? StoryPoints { get; set; }
        public Priority? Risk { get; set; }

        public UserStoryPlanningModifiedEvent(Priority priority, int? storyPoints, Priority? risk)
        {
            Priority = priority;
            StoryPoints = storyPoints;
            Risk = risk;
        }
    }
}