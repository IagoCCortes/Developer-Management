using System;
using DeveloperManagement.WorkItemManagement.Domain.Entities;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Application.Dtos
{
    public class RelatedWorkDto
    {
        public byte LinkTypeId { get; set; }
        public string Comment { get; set; }
        public string Url { get; set; }
        public Guid? WorkItemId { get; set; }

        public RelatedWork ToRelatedWork()
        {
            var linkType = (LinkType) LinkTypeId;
            return linkType == LinkType.GitHubIssue
                ? RelatedWork.CreateGitHubRelatedWork(new Link(Url), Comment)
                : RelatedWork.CreateWorkItemRelatedWork(linkType, Comment, WorkItemId!.Value);
        }
    }
}