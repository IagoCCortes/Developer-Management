using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        private static readonly Type TaskType = typeof(Task);

        private static readonly ConstructorInfo TaskConstructorInfo =
            TaskType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);

        private static readonly PropertyInfo EffortInfo = TaskType.GetProperty("Effort");
        private static readonly PropertyInfo IntegratedInBuildInfo = TaskType.GetProperty("IntegratedInBuild");
        private static readonly PropertyInfo StoryPointsInfo = TaskType.GetProperty("StoryPoints");
        private static readonly PropertyInfo SeverityInfo = TaskType.GetProperty("Severity");
        private static readonly PropertyInfo SystemInfoInfo = TaskType.GetProperty("SystemInfo");
        private static readonly PropertyInfo FoundInBuildInfo = TaskType.GetProperty("FoundInBuild");

        private static readonly Func<Task> TaskConstructor =
            Expression.Lambda<Func<Task>>(Expression.New(TaskConstructorInfo)).Compile();

        private static readonly Action<Task, Effort> SetEffortDelegate =
            (Action<Task, Effort>) Delegate.CreateDelegate(typeof(Action<Task, Effort>), EffortInfo.GetSetMethod()!);

        private static readonly Action<Task, string> SetIntegratedInBuildDelegate =
            (Action<Task, string>) Delegate.CreateDelegate(typeof(Action<Task, string>),
                IntegratedInBuildInfo.GetSetMethod(true)!);

        private static readonly Action<Task, int?> SetStoryPointsDelegate =
            (Action<Task, int?>) Delegate.CreateDelegate(typeof(Action<Task, int?>), StoryPointsInfo.GetSetMethod(true)!);

        private static readonly Action<Task, Priority> SetSeverityDelegate =
            (Action<Task, Priority>) Delegate.CreateDelegate(typeof(Action<Task, Priority>),
                SeverityInfo.GetSetMethod(true)!);

        private static readonly Action<Task, string> SetSystemInfoDelegate =
            (Action<Task, string>) Delegate.CreateDelegate(typeof(Action<Task, string>),
                SystemInfoInfo.GetSetMethod(true)!);

        private static readonly Action<Task, string> SetFoundInBuildDelegate =
            (Action<Task, string>) Delegate.CreateDelegate(typeof(Action<Task, string>),
                FoundInBuildInfo.GetSetMethod(true)!);

        public Task ToTask(WorkItemDao workItemDao, IEnumerable<TagDao> tags, IEnumerable<AttachmentDao> attachments,
            IEnumerable<RelatedWorkDao> relatedWorkDaos)
        {
            var task = TaskConstructor();
            WorkItemDao.PopulateBaseWorkItem(task, workItemDao, tags, attachments, relatedWorkDaos);
            SetEffortDelegate(task,
                new Effort(EffortOriginalEstimate!.Value, EffortRemaining!.Value, EffortCompleted!.Value));
            SetIntegratedInBuildDelegate(task, IntegratedInBuild);
            SetStoryPointsDelegate(task, StoryPoints);
            SetSeverityDelegate(task, (Priority) SeverityId);
            SetSystemInfoDelegate(task, SystemInfo);
            SetFoundInBuildDelegate(task, FoundInBuild);

            return task;
        }
    }
}