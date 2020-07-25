using FluentValidation;
using System;
using System.Linq;

namespace Hcqn.Core.Models.Extensions.CustomValidators
{
    public static class EnumerationCustomValidator
    {
        public static IRuleBuilderInitial<T, int?> IsEnumeration<T, TEnumeration>(this IRuleBuilder<T, int?> ruleBuilder, string errorMessage, bool isRequired = true)
            where TEnumeration : Enumeration
        {
            return ruleBuilder.Custom((id, context) =>
            {
                if (!isRequired && !id.HasValue)
                {
                    return;
                }

                if (isRequired && !id.HasValue)
                {
                    context.AddFailure(errorMessage);
                }

                var flag = Enumeration.GetAll<TEnumeration>().Any(x => x.Id == id);
                if (!flag)
                {
                    context.AddFailure(errorMessage);
                }
            });
        }

        public static IRuleBuilderInitial<T, int> IsEnumeration<T, TEnumeration>(this IRuleBuilder<T, int> ruleBuilder, string errorMessage)
            where TEnumeration : Enumeration
        {
            return ruleBuilder.Custom((id, context) =>
            {
                var flag = Enumeration.GetAll<TEnumeration>().Any(x => x.Id == id);
                if (!flag)
                {
                    context.AddFailure(errorMessage);
                }
            });
        }

        public static IRuleBuilderInitial<T, int> IsEnumeration<T, TEnumeration>(this IRuleBuilder<T, int> ruleBuilder, Func<TEnumeration, bool> expression, string errorMessage)
          where TEnumeration : Enumeration
        {
            return ruleBuilder.Custom((id, context) =>
            {
                var flag = expression.Invoke(Enumeration.FromValue<TEnumeration>(id));

                if (!flag)
                {
                    context.AddFailure(errorMessage);
                }
            });
        }
    }

}
