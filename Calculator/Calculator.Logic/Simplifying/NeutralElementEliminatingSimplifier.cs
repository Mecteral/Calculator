using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementEliminatingSimplifier : AVisitingTraversingReplacer
    {
        // TODO: do the same for division, and for addition/subtraction with zero
        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
        {
            return new NeutralElementOfMultiplicationRemover(multiplication).Transform();
        }
    }
}