using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Calculator.Logic.Utilities
{
    public static class LambdaExtensions
    {
        public static void SetTo<TClass, TProperty>(
            this Expression<Func<TClass, TProperty>> self,
            TClass target,
            TProperty value)
        {
            var memberSelectorExpression = self.Body as MemberExpression;
            var property = memberSelectorExpression?.Member as PropertyInfo;
            property?.SetValue(target, value, null);
        }
        public static TProperty GetFrom<TClass, TProperty>(this Expression<Func<TClass, TProperty>> self, TClass target)
        {
            return self.Compile()(target);
        }
    }
}