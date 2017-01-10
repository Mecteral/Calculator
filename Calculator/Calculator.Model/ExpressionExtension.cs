namespace Calculator.Model
{
    static class ExpressionExtension
    {
        public static IExpression Parent(this IExpression self) => ((AnExpression) self).Parent;
        public static IExpression Parent(this IExpression self, IExpression newParent)
            => ((AnExpression) self).Parent = newParent;
    }
}