using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Entities;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Helper;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("WorkItem")]
    public class WorkItemDao : DatabaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? AssignedTo { get; set; }
        public int StateId { get; set; }
        public Guid TeamId { get; set; }
        public Guid? SprintId { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public string RepoLink { get; set; }
        public int StateReasonId { get; set; }

        public WorkItemDao()
        {
        }

        public WorkItemDao(WorkItem workItem)
        {
            Id = workItem.Id;
            Title = workItem.Title;
            AssignedTo = workItem.AssignedTo;
            StateId = (int) workItem.State;
            TeamId = workItem.TeamId;
            SprintId = workItem.SprintId;
            Description = workItem.Description;
            PriorityId = (int) workItem.Priority;
            RepoLink = workItem.RepoLink?.Hyperlink;
            StateReasonId = (int) workItem.StateReason;
        }

        private static readonly Type EntityType = typeof(Entity);
        private static readonly Type WorkItemType = typeof(WorkItem);

        private static readonly PropertyInfo IdInfo = EntityType.GetProperty("Id");
        private static readonly PropertyInfo TitleInfo = WorkItemType.GetProperty("Title");
        private static readonly PropertyInfo AssignedToInfo = WorkItemType.GetProperty("AssignedTo");
        private static readonly PropertyInfo StateInfo = WorkItemType.GetProperty("State", typeof(WorkItemState));
        private static readonly PropertyInfo TeamIdInfo = WorkItemType.GetProperty("TeamId");
        private static readonly PropertyInfo SprintIdInfo = WorkItemType.GetProperty("SprintId");
        private static readonly PropertyInfo DescriptionInfo = WorkItemType.GetProperty("Description");
        private static readonly PropertyInfo PriorityInfo = WorkItemType.GetProperty("Priority");
        private static readonly PropertyInfo RepoLinkInfo = WorkItemType.GetProperty("RepoLink");
        private static readonly PropertyInfo StateReasonInfo = WorkItemType.GetProperty("StateReason");

        private static readonly FieldInfo TagsInfo =
            WorkItemType.GetField("_tags", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly FieldInfo AttachmentsInfo =
            WorkItemType.GetField("_attachments", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly FieldInfo RelatedWorksInfo =
            WorkItemType.GetField("_relatedWorks", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly Action<WorkItem, Guid> SetIdDelegate =
            (Action<WorkItem, Guid>) Delegate.CreateDelegate(typeof(Action<WorkItem, Guid>),
                IdInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, string> SetTitleDelegate =
            (Action<WorkItem, string>) Delegate.CreateDelegate(typeof(Action<WorkItem, string>),
                TitleInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, Guid?> SetAssignedToDelegate =
            (Action<WorkItem, Guid?>) Delegate.CreateDelegate(typeof(Action<WorkItem, Guid?>),
                AssignedToInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, WorkItemState> SetStateDelegate =
            (Action<WorkItem, WorkItemState>) Delegate.CreateDelegate(typeof(Action<WorkItem, WorkItemState>),
                StateInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, Guid> SetTeamIdDelegate =
            (Action<WorkItem, Guid>) Delegate.CreateDelegate(typeof(Action<WorkItem, Guid>),
                TeamIdInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, Guid?> SetSprintIdDelegate =
            (Action<WorkItem, Guid?>) Delegate.CreateDelegate(typeof(Action<WorkItem, Guid?>),
                SprintIdInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, string> SetDescriptionDelegate =
            (Action<WorkItem, string>) Delegate.CreateDelegate(typeof(Action<WorkItem, string>),
                DescriptionInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, Priority> SetPriorityDelegate =
            (Action<WorkItem, Priority>) Delegate.CreateDelegate(typeof(Action<WorkItem, Priority>),
                PriorityInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, StateReason> SetStateReasonDelegate =
            (Action<WorkItem, StateReason>) Delegate.CreateDelegate(typeof(Action<WorkItem, StateReason>),
                StateReasonInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, Link> SetRepoLinkDelegate =
            (Action<WorkItem, Link>) Delegate.CreateDelegate(typeof(Action<WorkItem, Link>),
                RepoLinkInfo.GetSetMethod(true)!);

        private static readonly Action<WorkItem, List<Tag>> SetTagsDelegate =
            ReflectionFieldGetterAndSetter.CreateSetter<WorkItem, List<Tag>>(TagsInfo);
        
        private static readonly Action<WorkItem, List<Attachment>> SetAttachmentsDelegate =
            ReflectionFieldGetterAndSetter.CreateSetter<WorkItem, List<Attachment>>(AttachmentsInfo);
        
        private static readonly Action<WorkItem, List<RelatedWork>> SetRelatedWorksDelegate =
            ReflectionFieldGetterAndSetter.CreateSetter<WorkItem, List<RelatedWork>>(RelatedWorksInfo);

        public static void PopulateBaseWorkItem(WorkItem workItem, WorkItemDao dao, IEnumerable<TagDao> tags,
            IEnumerable<AttachmentDao> attachments, IEnumerable<RelatedWorkDao> relatedWorkDaos)
        {
            SetIdDelegate(workItem, dao.Id);
            SetTitleDelegate(workItem, dao.Title);
            SetAssignedToDelegate(workItem, dao.AssignedTo);
            SetStateDelegate(workItem, (WorkItemState) dao.StateId);
            SetTeamIdDelegate(workItem, dao.TeamId);
            SetSprintIdDelegate(workItem, dao.SprintId);
            SetDescriptionDelegate(workItem, dao.Description);
            SetPriorityDelegate(workItem, (Priority) dao.PriorityId);
            SetStateReasonDelegate(workItem, (StateReason) dao.StateReasonId);
            if (!string.IsNullOrWhiteSpace(dao.RepoLink))
                SetRepoLinkDelegate(workItem, new Link(dao.RepoLink));

            SetTagsDelegate(workItem, tags.Select(t => t.ToTag()).ToList());
            SetAttachmentsDelegate(workItem, attachments.Select(a => a.ToAttachment()).ToList());
            SetRelatedWorksDelegate(workItem, relatedWorkDaos.Select(r => r.ToRelatedWork()).ToList());
        }
    }
}