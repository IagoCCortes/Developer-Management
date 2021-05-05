using DeveloperManagement.Core.Application;
using FluentValidation;

namespace DeveloperManagement.SprintManagement.Application.Sprints.Commands.CreateSprint
{
    public class CreateSprintCommandValidations : AbstractValidator<CreateSprintCommand>
    {
        public CreateSprintCommandValidations()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Sprint name cannot be empty");
            RuleFor(c => c.TeamId).EmptyGuid();
            RuleFor(c => c).Must(c => c.FinalDate > c.InitialDate)
                .WithMessage("Sprint's final date must be greater than initial's");
        }
    }
}