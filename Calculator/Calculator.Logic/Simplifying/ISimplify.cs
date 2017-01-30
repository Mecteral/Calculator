using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public interface ISimplify
    {
        IExpression Simplify(IExpression input);
    }
}