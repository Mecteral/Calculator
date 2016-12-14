namespace Calculator.Logic.Model
{
    public static class ExpressionExtensions
    {
        public static ParenthesedExpression Parenthesize(this IExpression self)
            => new ParenthesedExpression {Wrapped = self};
    }
}