using System;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Epic : WorkItem
    {
        public Planning Planning { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public Epic(string title, Guid area, ValueArea valueArea, Priority priority) : base(title, area, priority)
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
            public EpicBuilder(string title, Guid area, ValueArea valueArea = ValueArea.Business, Priority priority = Priority.Medium)
            {
                WorkItem = new Epic(title, area, valueArea, priority);
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