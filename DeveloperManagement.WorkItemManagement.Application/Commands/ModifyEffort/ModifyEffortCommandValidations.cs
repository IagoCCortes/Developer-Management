using DeveloperManagement.Core.Application;
using DeveloperManagement.Core.Application.Validations;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Commands.ModifyEffort
{
    public class ModifyEffortCommandValidations : AbstractValidator<ModifyEffortCommand>
    {
        public ModifyEffortCommandValidations()
        {
            RuleFor(c => c.Id).EmptyGuid();
        }
    }
}