﻿using System.Data;
using DeveloperManagement.Core.Application;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.SpecifyBugInfo
{
    public class SpecifyBugInfoCommandValidations : AbstractValidator<SpecifyBugInfoCommand>
    {
        public SpecifyBugInfoCommandValidations()
        {
            RuleFor(c => c.Id).EmptyGuid();
        }
    }
}