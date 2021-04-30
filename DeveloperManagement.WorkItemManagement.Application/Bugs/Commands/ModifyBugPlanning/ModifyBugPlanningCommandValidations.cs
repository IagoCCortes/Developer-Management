using System.Data;
using DeveloperManagement.Core.Application;
using DeveloperManagement.WorkItemManagement.Application.Common;
using DeveloperManagement.WorkItemManagement.Application.Dtos;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.UpdateBug
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