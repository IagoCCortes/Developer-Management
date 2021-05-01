using System;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.FeatureAggregate.Events;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.FeatureAggregate
{
    public class Feature : WorkItem
    {
        public Planning Planning { get; private set; }
        public ValueArea ValueArea { get; private set; }

        private Feature(string title, Guid teamId, ValueArea valueArea, Priority priority) : base(title, teamId, priority)
        {
            State = WorkItemState.New;
            StateReason = StateReason.New;
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
            
            DomainEvents.Add(new FeaturePlanningModifiedEvent(planning, priority));
        }

        public void SpecifyFeatureInfo(string description, ValueArea valueArea)
        {
            Description = description;
            ValueArea = valueArea;
            DomainEvents.Add(new FeatureInfoModifiedEvent(description, valueArea));
        }

        private StateReason SetStateReason(WorkItemState state)
            => StateReason = state switch
            {
                WorkItemState.New => StateReason.New,
                WorkItemState.Active => StateReason.ImplementationStarted,
                WorkItemState.Resolved => StateReason.StoriesComplete,
                WorkItemState.Closed => StateReason.AcceptanceTestsPass,
                WorkItemState.Removed => StateReason.RemovedFromTheBacklog,
            };
        
        public class FeatureBuilder : WorkItemBuilder<Feature>
        {
            public FeatureBuilder(string title, Guid teamId, ValueArea valueArea = ValueArea.Business, Priority priority = Priority.Medium)
            {
                WorkItem = new Feature(title, teamId, valueArea, priority);
            }

            public FeatureBuilder SetFeatureOptionalFields(Planning planning)
            {
                WorkItem.Planning = planning;
                return this;
            }

            public override Feature BuildWorkItem()
            {
                WorkItem.DomainEvents.Add(new FeatureCreatedEvent(WorkItem));
                return WorkItem;
            }
        }
    }
}