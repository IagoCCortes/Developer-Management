using System;
using System.Collections.Generic;
using System.Reflection;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("Bug")]
    public class BugDao : DatabaseEntity
    {
        public Guid Id { get; set; }
        public byte? EffortOriginalEstimate { get; set; }
        public byte? EffortRemaining { get; set; }
        public byte? EffortCompleted { get; set; }
        public string IntegratedInBuild { get; set; }
        public byte? StoryPoints { get; set; }
        public byte SeverityId { get; set; }
        public byte? ActivityId { get; set; }
        public string SystemInfo { get; set; }
        public string FoundInBuild { get; set; }

        public BugDao(Bug bug) : base(bug)
        {
            Id = bug.Id;
            EffortOriginalEstimate = bug.Effort?.OriginalEstimate;
            EffortRemaining = bug.Effort?.Remaining;
            EffortCompleted = bug.Effort?.Completed;
            IntegratedInBuild = bug.IntegratedInBuild;
            StoryPoints = bug.StoryPoints;
            SeverityId = (byte) bug.Severity;
            ActivityId = (byte?) bug.Activity;
            SystemInfo = bug.SystemInfo;
            FoundInBuild = bug.FoundInBuild;
        }

        public Bug ToBug(WorkItemDao workItemDao, List<TagDao> tags, List<CommentDao> comments,
            List<AttachmentDao> attachments, List<RelatedWorkDao> relatedWorkDaos)
        {
            var type = typeof(Bug);
            var bug = (Bug) Activator.CreateInstance(type, BindingFlags.NonPublic);
            WorkItemDao.PopulateBaseWorkItem(bug, workItemDao, tags, comments, attachments, relatedWorkDaos);
            if (EffortOriginalEstimate.HasValue)
                type.GetProperty("Effort").SetValue(bug,
                    new Effort(EffortOriginalEstimate.Value, EffortRemaining!.Value, EffortCompleted!.Value));
            type.GetProperty("IntegratedInBuild").SetValue(bug, IntegratedInBuild);
            type.GetProperty("StoryPoints").SetValue(bug, StoryPoints);
            type.GetProperty("Severity").SetValue(bug, (Priority) SeverityId);
            type.GetProperty("SystemInfo").SetValue(bug, SystemInfo);
            type.GetProperty("FoundInBuild").SetValue(bug, FoundInBuild);

            return bug;
        }
    }
}