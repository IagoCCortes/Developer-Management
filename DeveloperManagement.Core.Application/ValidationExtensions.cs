using System;
using FluentValidation;

namespace DeveloperManagement.Core.Application
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, Guid> EmptyGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder) =>
            ruleBuilder.Must(x => x != Guid.Empty).WithMessage("Invalid identifier");
        
        public static IRuleBuilderOptions<T, int?> NullableNotNegative<T>(this IRuleBuilder<T, int?> ruleBuilder, string field) =>
            ruleBuilder.Must(x => !x.HasValue || x.Value >= 0).WithMessage($"{field} cannot be empty");
    }
}