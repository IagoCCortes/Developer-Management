using System;
using System.Collections.Generic;
using System.Reflection;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.TaskAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("Task")]
    public class TaskDao : DatabaseEntity
    {
        public Guid Id { get; set; }
        public int? EffortOriginalEstimate { get; set; }
        public int? EffortRemaining { get; set; }
        public int? EffortCompleted { get; set; }
        public string IntegratedInBuild { get; set; }
        public int? StoryPoints { get; set; }
        public int SeverityId { get; set; }
        public int? ActivityId { get; set; }
        public string SystemInfo { get; set; }
        public string FoundInBuild { get; set; }

        public TaskDao()
        {
        }

        public TaskDao(Bug bug) : base(bug)
        {
            Id = bug.Id;
            EffortOriginalEstimate = bug.Effort?.OriginalEstimate;
            EffortRemaining = bug.Effort?.Remaining;
            EffortCompleted = bug.Effort?.Completed;
            IntegratedInBuild = bug.IntegratedInBuild;
            StoryPoints = bug.StoryPoints;
            SeverityId = (int) bug.Severity;
            ActivityId = (int?) bug.Activity;
            SystemInfo = bug.SystemInfo;
            FoundInBuild = bug.FoundInBuild;
        }

        public Task ToTask(WorkItemDao workItemDao, IEnumerable<TagDao> tags, IEnumerable<AttachmentDao> attachments,
            IEnumerable<RelatedWorkDao> relatedWorkDaos)
        {
            var type = typeof(Task);
            var task = (Task) type
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { }, null)
                .Invoke(null);
            WorkItemDao.PopulateBaseWorkItem(task, workItemDao, tags, attachments, relatedWorkDaos);
            type.GetProperty("Effort").SetValue(task,
                    new Effort(EffortOriginalEstimate.Value, EffortRemaining!.Value, EffortCompleted!.Value));
            type.GetProperty("IntegratedInBuild").SetValue(task, IntegratedInBuild);
            type.GetProperty("StoryPoints").SetValue(task, StoryPoints);
            type.GetProperty("Severity").SetValue(task, (Priority) SeverityId);
            type.GetProperty("SystemInfo").SetValue(task, SystemInfo);
            type.GetProperty("FoundInBuild").SetValue(task, FoundInBuild);

            return task;
        }
    }
}