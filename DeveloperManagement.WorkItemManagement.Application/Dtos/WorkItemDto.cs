using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.Core.Domain.Helper;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Dtos
{
    public class WorkItemDto
    {
        public string Title { get; set; }
        public Guid? AssignedTo { get; set; }
        public int? StateId { get; set; }
        public int? StateReasonId { get; set; }
        public Guid TeamId { get; set; }
        public Guid? SprintId { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public string RepoLink { get; set; }
        public IEnumerable<string> Comments { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<AttachmentDto> Attachments { get; set; }
        public IEnumerable<RelatedWorkDto> RelatedWorks { get; set; }
    }

    public static class WorkItemDtoValidations
    {
        public static IRuleBuilderOptions<T, string> Title<T>(this IRuleBuilder<T,string> ruleBuilder) {
            return ruleBuilder.NotEmpty().WithMessage("Title must not be empty");
        }
        
        public static IRuleBuilderOptions<T, Guid?> AssignedTo<T>(this IRuleBuilder<T,Guid?> ruleBuilder) {
            return ruleBuilder.Must(g => !g.HasValue || g != Guid.Empty)
                .WithMessage("Invalid Member identification");
        }
        
        public static IRuleBuilderOptions<T, int> StateId<T>(this IRuleBuilder<T,int> ruleBuilder) {
            return ruleBuilder.Must(s => Enum.IsDefined(typeof(WorkItemState), s))
                .WithMessage("Provided state not found");
        }
        
        public static IRuleBuilderOptions<T, int> StateReasonId<T>(this IRuleBuilder<T,int> ruleBuilder) {
            return ruleBuilder.Must(s => Enum.IsDefined(typeof(StateReason), s))
                .WithMessage("Provided state reason not found");
        }
        
        public static IRuleBuilderOptions<T, int> PriorityId<T>(this IRuleBuilder<T,int> ruleBuilder)
        {
            return ruleBuilder.Must(p => Enum.IsDefined(typeof(Priority), p))
                .WithMessage("Provided priority level not found");
        }
        
        public static IRuleBuilderOptions<T, Guid> TeamId<T>(this IRuleBuilder<T,Guid> ruleBuilder)
        {
            return ruleBuilder.Must(a => a != Guid.Empty)
                .WithMessage("Team identifier must not be empty");
        }
        
        public static IRuleBuilderOptions<T, Guid?> SprintId<T>(this IRuleBuilder<T,Guid?> ruleBuilder)
        {
            return ruleBuilder.Must(g => !g.HasValue || g != Guid.Empty)
                .WithMessage("Invalid Sprint identifier");
        }
        
        public static IRuleBuilderOptions<T, string> RepoLink<T>(this IRuleBuilder<T,string> ruleBuilder)
        {
            return ruleBuilder.Must(s => String.IsNullOrWhiteSpace(s) || s.IsStringAUrl())
                .WithMessage("Invalid Url");
        }
    }
}