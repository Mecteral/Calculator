using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Pipelines;
using FluentAssertions;
using Mecteral.UnitConversion;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Pipelines
{
    [TestFixture]
    public class PipelineEvaluatorTests
    {
        [SetUp]
        public void SetUp()
        {
            mApplicationArguments = Substitute.For<ApplicationArguments>();
            mConversionFactory = () => Substitute.For<IConversionFacade>();
            mSimplificationPipelineFactory = () => Substitute.For<ISimplificationPipeline>();
            mDecider = Substitute.For<IConsoleToMetricDecider>();
            mPipelineEvaluator = new PipelineEvaluator(mConversionFactory, mSimplificationPipelineFactory, mDecider);
        }

        IConsoleToMetricDecider mDecider;
        Func<IConversionFacade> mConversionFactory;
        Func<ISimplificationPipeline> mSimplificationPipelineFactory;
        ApplicationArguments mApplicationArguments;
        PipelineEvaluator mPipelineEvaluator;

        [Test]
        public void If_Input_Is_Null_Pipeline_Returns_Null()
        {
            string input = null;
            var result = mPipelineEvaluator.Evaluate(input, mApplicationArguments);
            result.Should().Be(null);
        }

        [Test]
        public void If_Input_Contains_ConversionSign_ConversionPipeline_Is_Called()
        {
            string input = "2+3=?";
            mPipelineEvaluator.Evaluate(input, mApplicationArguments);
        }
        [Test]
        public void If_Input_Contains_No_ConversionSign_SimpificationPipeline_Is_Called()
        {
            string input = "2+3";
            mPipelineEvaluator.Evaluate(input, mApplicationArguments);
        }
    }
}