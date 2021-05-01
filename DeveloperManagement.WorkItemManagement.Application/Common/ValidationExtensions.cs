using System;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Common
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, int?> Activity<T>(this IRuleBuilder<T, int?> ruleBuilder) =>
            ruleBuilder.Must(x => !x.HasValue || Enum.IsDefined(typeof(Activity), x.Value))
                .WithMessage("Provided activity not found");

        public static IRuleBuilderOptions<T, int> Priority<T>(this IRuleBuilder<T, int> ruleBuilder,
            string type = "priority") =>
            ruleBuilder.Must(p => Enum.IsDefined(typeof(Priority), p))
                .WithMessage($"Provided {type} level not found");
    }
}