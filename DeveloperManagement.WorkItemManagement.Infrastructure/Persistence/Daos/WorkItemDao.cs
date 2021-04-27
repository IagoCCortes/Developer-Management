using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("WorkItem")]
    public class WorkItemDao : DatabaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? AssignedTo { get; set; }
        public byte StateId { get; set; }
        public Guid AreaId { get; set; }
        public Guid? IterationId { get; set; }
        public string Description { get; set; }
        public byte PriorityId { get; set; }
        public string RepoLink { get; set; }
        public byte StateReasonId { get; set; }

        public WorkItemDao(WorkItem workItem)
        {
            Id = workItem.Id;
            Title = workItem.Title;
            AssignedTo = workItem.AssignedTo;
            StateId = (byte) workItem.State;
            AreaId = workItem.Area;
            IterationId = workItem.Iteration;
            Description = workItem.Description;
            PriorityId = (byte) workItem.Priority;
            RepoLink = workItem.RepoLink?.Hyperlink;
            StateReasonId = (byte) workItem.StateReason;
        }

        public static void PopulateBaseWorkItem(WorkItem workItem, WorkItemDao dao, List<TagDao> tags, List<CommentDao> comments,
            List<AttachmentDao> attachments, List<RelatedWorkDao> relatedWorkDaos)
        {
            var type = typeof(WorkItem);
            type.GetProperty("Id").SetValue(workItem, dao.Id);
            type.GetProperty("AssignedTo").SetValue(workItem, dao.AssignedTo);
            type.GetProperty("State", typeof(WorkItemState)).SetValue(workItem, (WorkItemState) dao.StateId);
            type.GetProperty("Area").SetValue(workItem, dao.AreaId);
            type.GetProperty("Iteration").SetValue(workItem, dao.IterationId);
            type.GetProperty("Description").SetValue(workItem, dao.Description);
            type.GetProperty("Priority").SetValue(workItem, (Priority) dao.PriorityId);
            if (!string.IsNullOrWhiteSpace(dao.RepoLink))
            {
                type.GetProperty("RepoLink").SetValue(workItem, new Link(dao.RepoLink));
            }
            type.GetProperty("StateReason").SetValue(workItem, (StateReason) dao.StateReasonId);
            type.GetProperty("_tags", BindingFlags.NonPublic)
                .SetValue(workItem, tags.Select(t => t.ToTag()));
            type.GetProperty("_comments", BindingFlags.NonPublic)
                .SetValue(workItem, comments.Select(c => c.ToComment()));
            type.GetProperty("_attachments", BindingFlags.NonPublic)
                .SetValue(workItem, attachments.Select(a => a.ToAttachment()));
            type.GetProperty("_relatedWorks", BindingFlags.NonPublic)
                .SetValue(workItem, relatedWorkDaos.Select(r => r.ToRelatedWork()));
        }
    }
}