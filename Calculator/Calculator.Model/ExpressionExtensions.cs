using System.Linq;

namespace Calculator.Model
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Only use this from inside Model!
        /// </summary>
        /// <param name="self"></param>
        /// <param name="newParent"></param>
        public static void Parent(this IExpression self, IExpression newParent)
            => ((AnExpression) self).Parent = newParent;
        public static ParenthesedExpression Parenthesize(this IExpression self)
            => new ParenthesedExpression {Wrapped = self};
        public static bool IsZero(this IExpression self)
        {
            var constant = self as Constant;
            return 0M == constant?.Value;
        }
        public static bool HasOnlyConstantChildren(this IExpression self) => self.Children.All(c => c is Constant);
        public static decimal GetConstantValue(this IExpression self) => ((Constant) self).Value;
    }
}