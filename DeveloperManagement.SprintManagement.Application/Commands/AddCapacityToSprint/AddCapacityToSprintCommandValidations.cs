using System;
using DeveloperManagement.Core.Application;
using DeveloperManagement.Core.Application.Validations;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using FluentValidation;

namespace DeveloperManagement.SprintManagement.Application.Commands.AddCapacityToSprint
{
    public class AddCapacityToSprintCommandValidations : AbstractValidator<AddCapacityToSprintCommand>
    {
        public AddCapacityToSprintCommandValidations()
        {
            RuleFor(c => c.Id).EmptyGuid();
            RuleFor(c => c.ActivityId).Must(c => Enum.IsDefined(typeof(Activity), c))
                .WithMessage("Provided Activity not found");
            RuleForEach(c => c.DaysOff).Must(p => p.FinalDateTime > p.InitialDateTime)
                .WithMessage("Period's final date must be greater than initial's");
            RuleFor(c => c.CapacityPerDay).GreaterThan(0).WithMessage("Daily capacity must be greater than 0");
        }
    }
}