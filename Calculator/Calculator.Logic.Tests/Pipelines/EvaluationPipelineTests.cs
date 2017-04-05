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
    public class EvaluationPipelineTests
    {
        [SetUp]
        public void SetUp()
        {
            mConversionFacade = Substitute.For<IConversionFacade>();
            mApplicationArguments = Substitute.For<IApplicationArguments>();
            mSimplificationPipeline = Substitute.For<ISimplificationPipeline>();
            mDecider = Substitute.For<IConsoleToMetricDecider>();
            mUnderTest = new EvaluationPipeline(() => mConversionFacade, () => mSimplificationPipeline, mDecider);
        }

        IConsoleToMetricDecider mDecider;
        IApplicationArguments mApplicationArguments;
        EvaluationPipeline mUnderTest;
        IConversionFacade mConversionFacade;
        ISimplificationPipeline mSimplificationPipeline;
        [Test]
        public void If_Input_Is_Null_Pipeline_Returns_Null()
        {
            var result = mUnderTest.Evaluate(null, mApplicationArguments);
            result.Should().Be(null);
        }

        [Test]
        public void If_Input_Contains_ConversionSign_ConversionPipeline_Is_Called()
        {
            var input = "2+3=?";
            mApplicationArguments.UnitForConversion.Returns("alpha");
            mApplicationArguments.ToMetric.Returns(true);
            mConversionFacade.ConvertUnits(input, "alpha", true).Returns("bravo");

            mUnderTest.Evaluate(input, mApplicationArguments).Should().Be("bravo");
            

        }
        [Test]
        public void If_Input_Contains_No_ConversionSign_SimpificationPipeline_Is_Called()
        {
            var input = "2+3";
            mSimplificationPipeline.UseSimplificationPipeline(input, mApplicationArguments).Returns("bravo");

            mUnderTest.Evaluate(input, mApplicationArguments).Should().Be("bravo");
        }

        [Test]
        public void Evaluate_For_Conversions_Calls_Decider()
        {
            var input = "2+3";
            mApplicationArguments.UseConversion.Returns(true);
            mApplicationArguments.UnitForConversion.Returns("alpha");
            mApplicationArguments.ToMetric.Returns(true);
            mConversionFacade.ConvertUnits(input, "alpha", true).Returns("bravo");

            mUnderTest.Evaluate(input, mApplicationArguments).Should().Be("bravo");

            mDecider.Received().Decide();
        }
    }
}