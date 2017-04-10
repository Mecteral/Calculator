using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public class ExpressionCounter : AVisitingEvaluator, IExpressionCounter
    {
        protected override void EvaluateMultiplication(Multiplication multiplication) => IncreaseCount();
        protected override void EvaluateAddition(Addition addition) => IncreaseCount();
        protected override void EvaluateConstant(Constant constant) => IncreaseCount();
        protected override void EvaluateDivision(Division division) => IncreaseCount();
        protected override void EvaluateVariable(Variable variable) => IncreaseCount();
        protected override void EvaluateCosine(Cosine cosineExpression) => IncreaseCount();
        protected override void EvaluateTangent(Tangent tangentExpression) => IncreaseCount();
        protected override void EvaluateSinus(Sinus sinusExpression) => IncreaseCount();
        protected override void EvaluatePower(Power power) => IncreaseCount();
        protected override void EvaluateParenthesed(ParenthesedExpression parenthesed) => IncreaseCount();
        protected override void EvaluateSubtraction(Subtraction subtraction) => IncreaseCount();
    }
}