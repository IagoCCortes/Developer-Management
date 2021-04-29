using System;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.WorkItemManagement.Application.Dtos;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.AddBug
{
    public class CreateBugCommandValidations : AbstractValidator<CreateBugCommand>
    {
        public CreateBugCommandValidations()
        {
            RuleFor(c => c.PriorityId).PriorityId();
            RuleFor(c => c.SeverityId).Must(p => Enum.IsDefined(typeof(Priority), p))
                .WithMessage("Provided severity level not found");
            RuleFor(c => c.AreaId).AreadId();
            RuleFor(c => c.Title).NotEmpty().WithMessage("Title must not be empty");
            RuleFor(c => c.StateReasonId).StateReasonId().Must(s =>
                    ((StateReason) s).IsOneOf(StateReason.New, StateReason.BuildFailure))
                .WithMessage("A new bug's state reason must be either \"New\" or \"Build Failure\"");
            RuleFor(c => c.AssignedTo).AssignedTo();
            RuleFor(c => c.IterationId).IterationId();
            RuleFor(c => c.RepoLink).RepoLink();
            RuleFor(c => new {c.OriginalEstimate, c.Remaining, c.Completed})
                .Must(ao => ao.OriginalEstimate.HasValue && ao.Remaining.HasValue && ao.Completed.HasValue)
                .When(c => c.OriginalEstimate.HasValue || c.Remaining.HasValue || c.Completed.HasValue)
                .WithMessage(
                    "When either 'Original Estimate', 'Remaining' or 'Completed' has a value all three must have a value");
            RuleForEach(c => c.Attachments).SetValidator(AttachmentDtoValidations.Validate().Filename().Path());
            RuleForEach(c => c.RelatedWorks).SetValidator(new RelatedWorkDtoValidations());
        }
    }
}