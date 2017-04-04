using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class MultiplicationByZeroRemovingSimplifier : AVisitingTraversingReplacer
    {
        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
        {
            if (multiplication.Left.IsZero() || multiplication.Right.IsZero()) return new Constant {Value = 0M};
            return multiplication;
        }
    }
}