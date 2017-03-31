using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementEliminatingSimplifier : ISimplifier
    {
        public IExpression Simplify(IExpression input)
        {
            var multiplication = input as Multiplication;
            if (null == multiplication) return input;
            // TODO: do the same for division, and for addition/subtraction with zero
            return new NeutralElementOfMultiplicationRemover(multiplication).Transform();
        }
    }
}