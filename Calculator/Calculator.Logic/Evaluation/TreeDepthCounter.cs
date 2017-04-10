using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public class TreeDepthCounter : AVisitingEvaluator, ITreeDepthCounter
    {
        void ChangeResultIfTreeDepthIsHigher(IExpression expression)
        {
            if (expression.TreeDepth > Result)
                Result = expression.TreeDepth;
        }

        protected override void EvaluateAddition(Addition addition) => ChangeResultIfTreeDepthIsHigher(addition);
        protected override void EvaluateDivision(Division division) => ChangeResultIfTreeDepthIsHigher(division);
        protected override void EvaluateMultiplication(Multiplication multiplication) => ChangeResultIfTreeDepthIsHigher(multiplication);
        protected override void EvaluatePower(Power power) => ChangeResultIfTreeDepthIsHigher(power);
        protected override void EvaluateSubtraction(Subtraction subtraction) => ChangeResultIfTreeDepthIsHigher(subtraction);
        protected override void EvaluateVariable(Variable variable) => ChangeResultIfTreeDepthIsHigher(variable);
        protected override void EvaluateConstant(Constant constant) => ChangeResultIfTreeDepthIsHigher(constant);
        protected override void EvaluateCosine(Cosine cosineExpression) => ChangeResultIfTreeDepthIsHigher(cosineExpression);
        protected override void EvaluateParenthesed(ParenthesedExpression parenthesed) => ChangeResultIfTreeDepthIsHigher(parenthesed);
        protected override void EvaluateSinus(Sinus sinusExpression) => ChangeResultIfTreeDepthIsHigher(sinusExpression);
        protected override void EvaluateTangent(Tangent tangentExpression) => ChangeResultIfTreeDepthIsHigher(tangentExpression);

    }
}