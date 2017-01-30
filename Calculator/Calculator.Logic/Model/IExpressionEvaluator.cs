using Calculator.Model;

namespace Calculator.Logic.Model
{
    public interface IExpressionEvaluator
    {
        decimal Evaluate(IExpression expression);
    }
}