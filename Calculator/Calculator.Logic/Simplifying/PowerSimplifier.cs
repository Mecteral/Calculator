using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class PowerSimplifier : AVisitingTraversingReplacer
    {
        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
        {
            if (multiplication.Left.GetConstantValue() == multiplication.Right.GetConstantValue())
                return new Power {Left = multiplication.Left, Right = multiplication.Right};
            return multiplication;
        }
    }
}