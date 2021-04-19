using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities
{
    public class RelatedWork : Entity
    {
        public LinkType LinkType { get; private set; }
        public string Comment { get; private set; }
        public Link Url { get; private set; }
        public Guid? WorkItemId{ get; private set; }
        
        private RelatedWork() {}

        private RelatedWork(LinkType linkType, string comment, Link url, Guid? workItemId)
        {
            LinkType = linkType;
            Comment = comment;
            Url = url;
            WorkItemId = workItemId;
        }

        public static RelatedWork CreateGitHubRelatedWork(Link url, string comment) =>
            new RelatedWork(LinkType.GitHubIssue, comment, url, null);

        public static RelatedWork CreateWorkItemRelatedWork(LinkType linkType, string comment, Guid workItemId)
            => new RelatedWork(linkType, comment, null, workItemId);

        public void ModifyComment(string comment)
        {
            Comment = comment;
        }
    }
}