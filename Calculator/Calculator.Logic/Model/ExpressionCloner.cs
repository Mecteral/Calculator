using Calculator.Model;

namespace Calculator.Logic.Model
{
    public class ExpressionCloner : AnExpressionVisitorWithResult<ExpressionCloner, IExpression>
    {
        public static IExpression Clone(IExpression expression) => GetResultFor(expression);
        protected override IExpression UseParenthesed(IExpression wrapped)
        {
            return new ParenthesedExpression {Wrapped = wrapped};
        }
        protected override IExpression UseSubtraction(IExpression left, IExpression right)
        {
            return new Subtraction {Left = left, Right = right};
        }
        protected override IExpression UseMultiplication(IExpression left, IExpression right)
        {
            return new Multiplication {Left = left, Right = right};
        }
        protected override IExpression UseAddition(IExpression left, IExpression right)
        {
            return new Addition {Left = left, Right = right};
        }
        protected override IExpression UseDivision(IExpression left, IExpression right)
        {
            return new Division {Left = left, Right = right};
        }
        protected override IExpression UseConstant(decimal value)
        {
            return new Constant {Value = value};
        }
        protected override IExpression UseVariable(string variable)
        {
            return new Variable {Variables = variable};
        }
    }
}