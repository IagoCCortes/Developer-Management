using DeveloperManagement.Core.Application;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.ModifyEffort
{
    public class ModifyEffortCommandValidations : AbstractValidator<ModifyEffortCommand>
    {
        public ModifyEffortCommandValidations()
        {
            RuleFor(c => c.Id).EmptyGuid();
            RuleFor(c => new {c.OriginalEstimate, c.Remaining, c.Completed})
                .Must(ao => ao.OriginalEstimate.HasValue && ao.Remaining.HasValue && ao.Completed.HasValue)
                .When(c => c.OriginalEstimate.HasValue || c.Remaining.HasValue || c.Completed.HasValue)
                .WithMessage(
                    "When either 'Original Estimate', 'Remaining' or 'Completed' has a value all three must have a value.");
        }
    }
}