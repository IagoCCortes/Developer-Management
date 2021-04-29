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
        public int LinkTypeId { get; set; }
        public string Comment { get; set; }
        public string Url { get; set; }
        public Guid? ReferencedWorkItemId { get; set; }
        public Guid WorkItemId { get; set; }

        public RelatedWorkDao()
        {
        }
        
        public RelatedWorkDao(RelatedWork entity, Guid workItemId) : base(entity)
        {
            Id = entity.Id;
            LinkTypeId = (int) entity.LinkType;
            Comment = entity.Comment;
            Url = entity.Url?.Hyperlink;
            WorkItemId = workItemId;
            ReferencedWorkItemId = entity.ReferencedWorkItemId;
        }
        
        public RelatedWork ToRelatedWork()
        {
            var linkType = (LinkType) LinkTypeId;
            var link = string.IsNullOrWhiteSpace(Url) ? null : new Link(Url);
            return linkType == LinkType.GitHubIssue
                ? RelatedWork.CreateGitHubRelatedWork(link, Url)
                : RelatedWork.CreateWorkItemRelatedWork(linkType, Comment, ReferencedWorkItemId!.Value);
        }
    }
}