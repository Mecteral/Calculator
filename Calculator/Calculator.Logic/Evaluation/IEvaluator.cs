using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public interface IEvaluator
    {
        int Evaluate(IExpression expression);
    }
}