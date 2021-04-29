using System;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.Core.Domain.Helper;
using DeveloperManagement.WorkItemManagement.Application.Dtos;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.AddBug
{
    public class CreateBugCommandValidations : AbstractValidator<CreateBugCommand>
    {
        public CreateBugCommandValidations()
        {
            RuleFor(c => c.PriorityId).Must(p => Enum.IsDefined(typeof(Priority), p))
                .WithMessage("Provided priority level not found");
            RuleFor(c => c.SeverityId).Must(p => Enum.IsDefined(typeof(Priority), p))
                .WithMessage("Provided severity level not found");
            RuleFor(c => c.AreaId).Must(a => a != Guid.Empty)
                .WithMessage("Area must not be empty");
            RuleFor(c => c.Title).NotEmpty().WithMessage("Title must not be empty");
            RuleFor(c => c.StateReasonId).Must(s => Enum.IsDefined(typeof(StateReason), s))
                .WithMessage("Provided state reason not found").Must(s =>
                    ((StateReason) s).IsOneOf(StateReason.New, StateReason.BuildFailure))
                .WithMessage("A new bug's state reason must be either \"New\" or \"Build Failure\"");
            RuleFor(c => c.AssignedTo).Must(g => g != Guid.Empty).When(c => c.AssignedTo.HasValue)
                .WithMessage("Invalid Member identification");
            RuleFor(c => c.IterationId).Must(g => g != Guid.Empty).When(c => c.IterationId.HasValue)
                .WithMessage("Invalid Iteration identification");
            RuleFor(c => c.RepoLink).Must(s => s.IsStringAUrl()).When(c => !String.IsNullOrWhiteSpace(c.RepoLink))
                .WithMessage("Invalid Url");
            RuleFor(c => new {c.OriginalEstimate, c.Remaining, c.Completed})
                .Must(ao => ao.OriginalEstimate.HasValue && ao.Remaining.HasValue && ao.Completed.HasValue)
                .When(c => c.OriginalEstimate.HasValue || c.Remaining.HasValue || c.Completed.HasValue)
                .WithMessage(
                    "When either 'Original Estimate', 'Remaining' or 'Completed' has a value all three must have a value");
            RuleForEach(c => c.Attachments).SetValidator(new AttachmentDtoValidations());
            RuleForEach(c => c.RelatedWorks).SetValidator(new RelatedWorkDtoValidations());
        }
    }
}