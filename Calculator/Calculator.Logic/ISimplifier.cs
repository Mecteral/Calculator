using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public interface ISimplifier
    {
        IExpression Simplify(IExpression input);
    }
}