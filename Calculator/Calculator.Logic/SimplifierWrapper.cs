using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public class SimplifierWrapper : ISimplifier
    {
        public IExpression Simplify(IExpression input)
        {
            var simplifier= new Simplifier(input);
            return simplifier.Simplify();
        }
    }
}