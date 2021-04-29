using System;
using DeveloperManagement.Core.Domain.Helper;
using DeveloperManagement.WorkItemManagement.Domain.Entities;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Dtos
{
    public class RelatedWorkDto
    {
        public int LinkTypeId { get; set; }
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

    public class RelatedWorkDtoValidations : AbstractValidator<RelatedWorkDto>
    {
        public RelatedWorkDtoValidations()
        {
            RuleFor(r => r.LinkTypeId).Must(l => Enum.IsDefined(typeof(LinkType), l))
                .WithMessage("Provided Link type not found");
            RuleFor(r => r.Url).Must(u => !string.IsNullOrWhiteSpace(u) && u.IsStringAUrl())
                .When(r => (LinkType) r.LinkTypeId == LinkType.GitHubIssue)
                .WithMessage("A Github issue requires a valid Url");
            RuleFor(r => r.WorkItemId).Must(w => w.HasValue && w != Guid.Empty)
                .When(r => (LinkType) r.LinkTypeId != LinkType.GitHubIssue)
                .WithMessage("Invalid work item identifier");
        }
    }
}