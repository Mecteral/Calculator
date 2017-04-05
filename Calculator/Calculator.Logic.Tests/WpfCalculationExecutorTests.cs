using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Pipelines;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class WpfCalculationExecutorTests
    {
        [SetUp]
        public void SetUp()
        {
            mEvaluationPipeline = Substitute.For<IEvaluationPipeline>();
            mApplicationArguments = Substitute.For<IApplicationArguments>();
            mUnderTest = new WpfCalculationExecutor(mEvaluationPipeline);
        }

        WpfCalculationExecutor mUnderTest;
        IApplicationArguments mApplicationArguments;
        IEvaluationPipeline mEvaluationPipeline;

        [Test]
        public void InitiateCalculation_Sets_Result()
        {
            mEvaluationPipeline.Evaluate("", mApplicationArguments).Returns("alpha");
            mUnderTest.InitiateCalculation("", mApplicationArguments);
            mUnderTest.CalculationResult.Should().Be("alpha");
        }

        [Test]
        public void InitiateCalculation_Sets_Steps()
        {
            mEvaluationPipeline.Evaluate("", mApplicationArguments).Returns("alpha");
            mUnderTest.InitiateCalculation("", mApplicationArguments);
            //mUnderTest.CalculationSteps.Should().Contain("alpha");
        }
    }
}