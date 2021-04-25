using System;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Epic : WorkItem
    {
        public byte? Effort { get; private set; }
        public byte? BusinessValue { get; private set; }
        public byte? TimeCriticality { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? TargetDate { get; private set; }
        public Priority? Risk { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public Epic(string title, Guid area, byte? effort, byte? businessValue,
            byte? timeCriticality, DateTime? startDate, DateTime? targetDate, Priority? risk,
            ValueArea valueArea = ValueArea.Business,
            Priority priority = Priority.Medium)
            : base(title, area, priority)
        {
            State = WorkItemState.New;
            StateReason = StateReason.AddedToBacklog;
            Effort = effort;
            BusinessValue = businessValue;
            TimeCriticality = timeCriticality;
            StartDate = startDate;
            TargetDate = targetDate;
            Risk = risk;
            ValueArea = valueArea;
        }

        public override void ModifyState(WorkItemState state)
        { 
            SetStateReason(state);
            base.ModifyState(state);
        }

        public void ModifyEpicEffort(byte? epicEffort)
        {
            Effort = epicEffort;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<byte?>(nameof(Effort), epicEffort));
        }

        public void ModifyBusinessValue(byte? businessValue)
        {
            BusinessValue = businessValue;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<byte?>(nameof(BusinessValue), businessValue));
        }

        public void ModifyTimeCriticality(byte? timeCriticality)
        {
            TimeCriticality = timeCriticality;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<byte?>(nameof(TimeCriticality), timeCriticality));
        }

        public void ModifyStartDate(DateTime? startDate)
        {
            StartDate = startDate;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<DateTime?>(nameof(StartDate), startDate));
        }

        public void ModifyTargetDate(DateTime? targetDate)
        {
            TargetDate = targetDate;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<DateTime?>(nameof(TargetDate), targetDate));
        }

        public void ModifyRisk(Priority? risk)
        {
            Risk = risk;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Priority?>(nameof(Risk), risk));
        }

        public void ModifyValueArea(ValueArea valueArea)
        {
            ValueArea = valueArea;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<ValueArea>(nameof(ValueArea), valueArea));
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
    }
}