using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public class AdditiveCounter : AVisitingEvaluator
    {
        protected override void EvaluateAddition(Addition addition)
        {
            IncreaseCount();
        }

        protected override void EvaluateSubtraction(Subtraction subtraction)
        {
            IncreaseCount();
        }
    }
}