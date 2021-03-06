using DeveloperManagement.Core.Application;
using DeveloperManagement.Core.Application.Validations;
using DeveloperManagement.WorkItemManagement.Application.Common;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Commands.ModifyBugPlanning
{
    public class ModifyBugPlanningCommandValidations : AbstractValidator<ModifyBugPlanningCommand>
    {
        public ModifyBugPlanningCommandValidations()
        {
            RuleFor(c => c.Id).EmptyGuid();
            RuleFor(c => c.ActivityId).Activity();
            RuleFor(c => c.PriorityId).Priority();
            RuleFor(c => c.SeverityId).Priority("severity");
            RuleFor(c => c.StoryPoints).NullableNotNegative("story points");
        }
    }
}