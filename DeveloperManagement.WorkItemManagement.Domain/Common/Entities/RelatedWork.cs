using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.Entities
{
    public class RelatedWork : Entity
    {
        public LinkType LinkType { get; private set; }
        public string Comment { get; private set; }
        public Link Url { get; private set; }
        public Guid? ReferencedWorkItemId { get; private set; }
        
        private RelatedWork() {}
        private RelatedWork(LinkType linkType, string comment, Link url, Guid? referencedWorkItemId)
        {
            LinkType = linkType;
            Comment = comment;
            Url = url;
            ReferencedWorkItemId = referencedWorkItemId;
        }

        public static RelatedWork CreateGitHubRelatedWork(Link url, string comment) =>
            new RelatedWork(LinkType.GitHubIssue, comment, url, null);

        // verificar se o id do work item existe na camada de application
        public static RelatedWork CreateWorkItemRelatedWork(LinkType linkType, string comment, Guid workItemId)
            => new RelatedWork(linkType, comment, null, workItemId);

        public void ModifyComment(string comment)
        {
            Comment = comment;
        }
    }
}