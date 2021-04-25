using System;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class WorkItemCreatedEvent
    {
        public string Title { get; }
        public WorkItemState State { get; }
        public Guid Area { get; }
        public Guid? Iteration { get; }
        public Priority Priority { get; }

        public WorkItemCreatedEvent(string title, WorkItemState state, Guid area, Guid? iteration, Priority priority)
        {
            Title = title;
            State = state;
            Area = area;
            Iteration = iteration;
            Priority = priority;
        }
    }
}