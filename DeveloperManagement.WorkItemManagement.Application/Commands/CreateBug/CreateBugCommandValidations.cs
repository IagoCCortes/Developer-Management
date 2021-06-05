using System;
using DeveloperManagement.Core.Domain.Extensions;
using DeveloperManagement.WorkItemManagement.Application.Dtos;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Commands.CreateBug
{
    public class CreateBugCommandValidations : AbstractValidator<CreateBugCommand>
    {
        public CreateBugCommandValidations()
        {
            RuleFor(c => c.PriorityId).PriorityId();
            RuleFor(c => c.SeverityId).Must(p => Enum.IsDefined(typeof(Priority), p))
                .WithMessage("Provided severity level not found");
            RuleFor(c => c.TeamId).TeamId();
            RuleFor(c => c.Title).NotEmpty().WithMessage("Title must not be empty");
            RuleFor(c => c.StateReasonId).StateReasonId().Must(s =>
                    ((StateReason) s).IsOneOf(StateReason.New, StateReason.BuildFailure))
                .WithMessage("A new bug's state reason must be either \"New\" or \"Build Failure\"");
            RuleFor(c => c.AssignedTo).AssignedTo();
            RuleFor(c => c.SprintId).SprintId();
            RuleFor(c => c.RepoLink).RepoLink();
            RuleForEach(c => c.Attachments).SetValidator(AttachmentDtoValidations.Validate().Filename().Path());
            RuleForEach(c => c.RelatedWorks).SetValidator(new RelatedWorkDtoValidations());
        }
    }
}