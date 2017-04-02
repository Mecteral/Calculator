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

        protected override IExpression UseCosine(decimal value)
        {
            return new Cosine {Value = value};
        }

        protected override IExpression UseTangent(decimal value)
        {
            return new Tangent { Value = value };
        }

        protected override IExpression UseSinus(decimal value)
        {
            return new Sinus { Value = value };
        }

        protected override IExpression UseVariable(string variable)
        {
            return new Variable {Variables = variable};
        }

        protected override IExpression UseSquare(IExpression left, IExpression right)
        {
            return new Power { Left = left, Right = right };
        }
    }
}