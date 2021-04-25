using System;
using System.Reflection;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("RelatedWork")]
    public class RelatedWorkDao : DatabaseEntity
    {
        public Guid Id { get; set; }
        public byte LinkTypeId { get; set; }
        public string Comment { get; set; }
        public string Url { get; set; }
        public Guid? ReferencedWorkItemId { get; set; }
        public Guid WorkItemId { get; set; }
        
        public RelatedWorkDao(RelatedWork entity, Guid workItemId) : base(entity)
        {
            Id = entity.Id;
            LinkTypeId = (byte) entity.LinkType;
            Comment = entity.Comment;
            Url = entity.Url?.Hyperlink;
            WorkItemId = workItemId;
            ReferencedWorkItemId = entity.ReferencedWorkItemId;
        }
        
        public RelatedWork ToRelatedWork()
        {
            var type = typeof(RelatedWork);
            var relatedWork = (RelatedWork)Activator.CreateInstance(type, BindingFlags.NonPublic);
            type.GetProperty("LinkType").SetValue(relatedWork, (LinkType)LinkTypeId);
            type.GetProperty("Comment").SetValue(relatedWork, Comment);
            type.GetProperty("Url").SetValue(relatedWork, new Link(Url));
            type.GetProperty("ReferencedWorkItemId").SetValue(relatedWork, ReferencedWorkItemId);

            return relatedWork;
        }
    }
}