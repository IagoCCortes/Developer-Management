using System;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public class Feature : WorkItem
    {
        public byte? Effort { get; private set; }
        public byte? BusinessValue { get; private set; }
        public byte? TimeCriticality { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? TargetDate { get; private set; }
        public Priority? Risk { get; private set; }
        public ValueArea ValueArea { get; private set; }

        public Feature(string title, Guid area, Guid iteration, byte? effort,
            byte? businessValue, byte? timeCriticality, DateTime? startDate, DateTime? targetDate, Priority? risk,
            ValueArea valueArea = ValueArea.Business, Priority priority = Priority.Medium) : base(title, area,
            iteration, priority)
        {
            ModifyState(WorkItemState.New);
            Effort = effort;
            BusinessValue = businessValue;
            TimeCriticality = timeCriticality;
            StartDate = startDate;
            TargetDate = targetDate;
            Risk = risk;
            ValueArea = valueArea;
        }

        public void ModifyState(WorkItemState state)
        {
            SetStateReason(state);
            State = state;
        }
        
        public void ModifyEpicEffort(byte? epicEffort) => Effort = epicEffort;
        
        public void ModifyBusinessValue(byte? businessValue) => BusinessValue = businessValue;

        public void ModifyTimeCriticality(byte? timeCriticality) => TimeCriticality = timeCriticality;
        
        public void ModifyStartDate(DateTime? startDate) => StartDate = startDate;
        
        public void ModifyTargetDate(DateTime? targetDate) => TargetDate = targetDate;
        
        public void ModifyRisk(Priority? risk) => Risk = risk;
        
        public void ModifyValueArea(ValueArea valueArea) => ValueArea = valueArea;

        private void SetStateReason(WorkItemState state)
            => StateReason = state switch
            {
                WorkItemState.New => StateReason.New,
                WorkItemState.Active => StateReason.ImplementationStarted,
                WorkItemState.Resolved => StateReason.StoriesComplete,
                WorkItemState.Closed => StateReason.AcceptanceTestsPass,
                WorkItemState.Removed => StateReason.RemovedFromTheBacklog,
            };
    }
}