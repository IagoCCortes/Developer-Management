using System;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.EpicAggregate.Events;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.EpicAggregate
{
    public class Epic : WorkItem
    {
        public Planning Planning { get; private set; }
        public ValueArea ValueArea { get; private set; }

        private Epic(string title, Guid teamId, ValueArea valueArea, Priority priority) : base(title, teamId, priority)
        {
            State = WorkItemState.New;
            StateReason = StateReason.AddedToBacklog;
            ValueArea = valueArea;
        }

        public override void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            base.ModifyState(state);
        }

        public void ModifyPlanning(Planning planning, Priority priority)
        {
            Priority = priority;
            Planning = planning;
            
            DomainEvents.Add(new EpicPlanningModifiedEvent(planning, priority));
        }

        public void SpecifyEpicInfo(string description, ValueArea valueArea)
        {
            Description = description;
            ValueArea = valueArea;
            DomainEvents.Add(new EpicInfoModifiedEvent(description, valueArea));
        }

        private StateReason SetStateReason(WorkItemState state)
            => StateReason = state switch
            {
                WorkItemState.New => StateReason.AddedToBacklog,
                WorkItemState.Active => StateReason.ImplementationStarted,
                WorkItemState.Resolved => StateReason.FeaturesComplete,
                WorkItemState.Closed => StateReason.AcceptanceTestsPass,
                WorkItemState.Removed => StateReason.RemovedFromTheBacklog,
            };
        
        public class EpicBuilder : WorkItemBuilder<Epic>
        {
            public EpicBuilder(string title, Guid teamId, ValueArea valueArea = ValueArea.Business, Priority priority = Priority.Medium)
            {
                WorkItem = new Epic(title, teamId, valueArea, priority);
            }

            public EpicBuilder SetEpicOptionalFields(Planning planning)
            {
                WorkItem.Planning = planning;
                return this;
            }

            public override Epic BuildWorkItem()
            {
                WorkItem.DomainEvents.Add(new EpicCreatedEvent(WorkItem));
                return WorkItem;
            }
        }
    }
}