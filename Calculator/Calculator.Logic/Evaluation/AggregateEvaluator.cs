using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public class AggregateEvaluator : IAggregateEvaluator
    {
        readonly IAdditiveCounter mAdditiveCounter;
        readonly IExpressionCounter mExpressionCounter;
        readonly ITreeDepthSetter mTreeDepthSetter;
        readonly IParenthesesCounter mParenthesesCounter;
        readonly ITreeDepthCounter mTreeDepthCounter;
        int mResult;

        public AggregateEvaluator(IParenthesesCounter parenthesesCounter, IAdditiveCounter additiveCounter,
            ITreeDepthCounter treeDepthCounter, IExpressionCounter expressionCounter, ITreeDepthSetter treeDepthSetter)
        {
            mResult = 0;
            mParenthesesCounter = parenthesesCounter;
            mAdditiveCounter = additiveCounter;
            mTreeDepthCounter = treeDepthCounter;
            mExpressionCounter = expressionCounter;
            mTreeDepthSetter = treeDepthSetter;
        }

        public int Evaluate(IExpression expression)
        {
            mTreeDepthSetter.SetTreeDepth(expression);
            mResult += mParenthesesCounter.Evaluate(expression) * 10;
            mResult += mAdditiveCounter.Evaluate(expression) * 5;
            mResult += mTreeDepthCounter.Evaluate(expression) * 2;
            mResult += mExpressionCounter.Evaluate(expression) * 8;
            return mResult;
        }
    }
}