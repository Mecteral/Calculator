using System;
using System.ComponentModel;
using Calculator.Model;

namespace Calculator.Logic.Evaluation
{
    public class AggregateEvaluator : IAggregateEvaluator
    {
        readonly IEvaluator mParenthesesCounter;
        readonly IEvaluator mAdditiveCounter;
        readonly IEvaluator mTreeDepthCounter;
        readonly IEvaluator mExpressionCounter;
        int mResult;

        public AggregateEvaluator(IEvaluator parenthesesCounter, IEvaluator additiveCounter,
            IEvaluator treeDepthCounter, IEvaluator expressionCounter)
        {
            mResult = 0;
            mParenthesesCounter = parenthesesCounter;
            mAdditiveCounter = additiveCounter;
            mTreeDepthCounter = treeDepthCounter;
            mExpressionCounter = expressionCounter;
        }
        public int Evaluate(IExpression expression)
        {
            mResult += mParenthesesCounter.Evaluate(expression) * 10;
            mResult += mAdditiveCounter.Evaluate(expression) * 5;
            mResult += mTreeDepthCounter.Evaluate(expression) * 2;
            mResult += mExpressionCounter.Evaluate(expression) * 8;
            return mResult;
        }
    }

    public interface IAggregateEvaluator : IEvaluator {}
}