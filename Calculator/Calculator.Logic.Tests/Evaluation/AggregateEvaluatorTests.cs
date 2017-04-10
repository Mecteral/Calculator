using Calculator.Logic.Evaluation;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Evaluation
{
    [TestFixture]
    public class AggregateEvaluatorTests
    {
        [SetUp]
        public void SetUp()
        {
            mParenthesesCounter = new ParenthesesCounter();
            mAdditiveCounter = new AdditiveCounter();
            mTreeDepthCounter = new TreeDepthCounter();
            mExpressionCounter = new ExpressionCounter();
            mTreeDepthSetter = new TreeDepthSetter();
            mUnderTest = new AggregateEvaluator(mParenthesesCounter, mAdditiveCounter, mTreeDepthCounter,
                mExpressionCounter, mTreeDepthSetter);
        }

        ITreeDepthSetter mTreeDepthSetter;
        ParenthesesCounter mParenthesesCounter;
        AdditiveCounter mAdditiveCounter;
        TreeDepthCounter mTreeDepthCounter;
        ExpressionCounter mExpressionCounter;
        AggregateEvaluator mUnderTest;

        [Test]
        public void Simple_Case()
        {
            var input = new Addition {Left = new Constant(), Right = new Constant()};
            var result = mUnderTest.Evaluate(input);
            result.Should().Be(31);
        }
    }
}