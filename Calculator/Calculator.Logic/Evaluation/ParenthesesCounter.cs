using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public class ParenthesesCounter : AVisitingEvaluator, IParenthesesCounter
    {
        protected override void EvaluateParenthesed(ParenthesedExpression parenthesed) => IncreaseCount();
    }
}