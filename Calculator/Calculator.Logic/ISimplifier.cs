using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public interface ISimplifier {
        IExpression Simplify();
    }
}