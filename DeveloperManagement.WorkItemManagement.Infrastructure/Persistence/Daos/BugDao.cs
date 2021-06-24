using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("Bug")]
    public class BugDao : DatabaseEntity
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

        public BugDao()
        {
        }

        public BugDao(Bug bug) : base(bug)
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
        
        private static readonly Type BugType = typeof(Bug);

        private static readonly ConstructorInfo BugConstructorInfo =
            BugType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);

        private static readonly PropertyInfo EffortInfo = BugType.GetProperty("Effort");
        private static readonly PropertyInfo IntegratedInBuildInfo = BugType.GetProperty("IntegratedInBuild");
        private static readonly PropertyInfo StoryPointsInfo = BugType.GetProperty("StoryPoints");
        private static readonly PropertyInfo SeverityInfo = BugType.GetProperty("Severity");
        private static readonly PropertyInfo SystemInfoInfo = BugType.GetProperty("SystemInfo");
        private static readonly PropertyInfo FoundInBuildInfo = BugType.GetProperty("FoundInBuild");
        
        private static readonly Func<Bug> BugConstructor = Expression.Lambda<Func<Bug>>(
            Expression.New(BugConstructorInfo)).Compile();

        private static readonly Action<Bug, Effort> SetEffortDelegate =
            (Action<Bug, Effort>) Delegate.CreateDelegate(typeof(Action<Bug, Effort>), EffortInfo.GetSetMethod(true)!);
        private static readonly Action<Bug, string> SetIntegratedInBuildDelegate =
            (Action<Bug, string>) Delegate.CreateDelegate(typeof(Action<Bug, string>), IntegratedInBuildInfo.GetSetMethod(true)!);
        private static readonly Action<Bug, int?> SetStoryPointsDelegate =
            (Action<Bug, int?>) Delegate.CreateDelegate(typeof(Action<Bug, int?>), StoryPointsInfo.GetSetMethod(true)!);
        private static readonly Action<Bug, Priority> SetSeverityDelegate =
            (Action<Bug, Priority>) Delegate.CreateDelegate(typeof(Action<Bug, Priority>), SeverityInfo.GetSetMethod(true)!);
        private static readonly Action<Bug, string> SetSystemInfoDelegate =
            (Action<Bug, string>) Delegate.CreateDelegate(typeof(Action<Bug, string>), SystemInfoInfo.GetSetMethod(true)!);
        private static readonly Action<Bug, string> SetFoundInBuildDelegate =
            (Action<Bug, string>) Delegate.CreateDelegate(typeof(Action<Bug, string>), FoundInBuildInfo.GetSetMethod(true)!);

        public Bug ToBug(WorkItemDao workItemDao, IEnumerable<TagDao> tags, IEnumerable<AttachmentDao> attachments,
            IEnumerable<RelatedWorkDao> relatedWorkDaos)
        {
            var bug = BugConstructor();
            
            WorkItemDao.PopulateBaseWorkItem(bug, workItemDao, tags, attachments, relatedWorkDaos);
            SetEffortDelegate(bug,
                new Effort(EffortOriginalEstimate!.Value, EffortRemaining!.Value, EffortCompleted!.Value));
            SetIntegratedInBuildDelegate(bug, IntegratedInBuild);
            SetStoryPointsDelegate(bug, StoryPoints);
            SetSeverityDelegate(bug, (Priority) SeverityId);
            SetSystemInfoDelegate(bug, SystemInfo);
            SetFoundInBuildDelegate(bug, FoundInBuild);

            return bug;
        }
    }
}