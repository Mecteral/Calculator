namespace Calculator.Model
{
    static class ExpressionExtension
    {
        public static void Parent(this IExpression self, IExpression newParent)
            => ((AnExpression) self).Parent = newParent;
    }
}