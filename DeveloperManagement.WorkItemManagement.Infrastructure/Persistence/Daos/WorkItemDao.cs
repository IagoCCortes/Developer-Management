﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeveloperManagement.Core.Domain;
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
        public int StateId { get; set; }
        public Guid AreaId { get; set; }
        public Guid? IterationId { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public string RepoLink { get; set; }
        public int StateReasonId { get; set; }
        
        public WorkItemDao() {}

        public WorkItemDao(WorkItem workItem)
        {
            Id = workItem.Id;
            Title = workItem.Title;
            AssignedTo = workItem.AssignedTo;
            StateId = (int) workItem.State;
            AreaId = workItem.Area;
            IterationId = workItem.Iteration;
            Description = workItem.Description;
            PriorityId = (int) workItem.Priority;
            RepoLink = workItem.RepoLink?.Hyperlink;
            StateReasonId = (int) workItem.StateReason;
        }

        public static void PopulateBaseWorkItem(WorkItem workItem, WorkItemDao dao, IEnumerable<TagDao> tags, IEnumerable<CommentDao> comments,
            IEnumerable<AttachmentDao> attachments, IEnumerable<RelatedWorkDao> relatedWorkDaos)
        {
            var entityType = typeof(Entity);
            var workItemType = typeof(WorkItem);
            entityType.GetProperty("Id").SetValue(workItem, dao.Id);
            workItemType.GetProperty("Title").SetValue(workItem, dao.Title);
            workItemType.GetProperty("AssignedTo").SetValue(workItem, dao.AssignedTo);
            workItemType.GetProperty("State", typeof(WorkItemState)).SetValue(workItem, (WorkItemState) dao.StateId);
            workItemType.GetProperty("Area").SetValue(workItem, dao.AreaId);
            workItemType.GetProperty("Iteration").SetValue(workItem, dao.IterationId);
            workItemType.GetProperty("Description").SetValue(workItem, dao.Description);
            workItemType.GetProperty("Priority").SetValue(workItem, (Priority) dao.PriorityId);
            if (!string.IsNullOrWhiteSpace(dao.RepoLink))
            {
                workItemType.GetProperty("RepoLink").SetValue(workItem, new Link(dao.RepoLink));
            }
            workItemType.GetProperty("StateReason").SetValue(workItem, (StateReason) dao.StateReasonId);
            workItemType.GetField("_tags", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(workItem, tags.Select(t => t.ToTag()).ToList());
            workItemType.GetField("_comments", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(workItem, comments.Select(c => c.ToComment()).ToList());
            workItemType.GetField("_attachments", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(workItem, attachments.Select(a => a.ToAttachment()).ToList());
            workItemType.GetField("_relatedWorks", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(workItem, relatedWorkDaos.Select(r => r.ToRelatedWork()).ToList());
        }
    }
}