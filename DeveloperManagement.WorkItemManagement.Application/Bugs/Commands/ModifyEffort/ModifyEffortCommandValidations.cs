using DeveloperManagement.Core.Application;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.ModifyEffort
{
    public class ModifyEffortCommandValidations : AbstractValidator<ModifyEffortCommand>
    {
        public ModifyEffortCommandValidations()
        {
            RuleFor(c => c.Id).EmptyGuid();
        }
    }
}