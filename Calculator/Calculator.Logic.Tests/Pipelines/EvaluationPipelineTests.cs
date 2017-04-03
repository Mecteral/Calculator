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
            mConversionFactory = () => mConversionFacade;
            mSimplificationPipelineFactory = () => Substitute.For<ISimplificationPipeline>();
            mDecider = Substitute.For<IConsoleToMetricDecider>();
            mUnderTest = new EvaluationPipeline(mConversionFactory, mSimplificationPipelineFactory, mDecider);
        }

        IConsoleToMetricDecider mDecider;
        Func<IConversionFacade> mConversionFactory;
        Func<ISimplificationPipeline> mSimplificationPipelineFactory;
        IApplicationArguments mApplicationArguments;
        EvaluationPipeline mUnderTest;
        IConversionFacade mConversionFacade;
        [Test]
        public void If_Input_Is_Null_Pipeline_Returns_Null()
        {
            string input = null;
            var result = mUnderTest.Evaluate(input, mApplicationArguments);
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
            string input = "2+3";
            mUnderTest.Evaluate(input, mApplicationArguments);
        }
    }
}