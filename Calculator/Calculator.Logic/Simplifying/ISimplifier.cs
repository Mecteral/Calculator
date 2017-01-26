using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public interface ISimplifier
    {
        IExpression Simplify(IExpression input);
    }
}