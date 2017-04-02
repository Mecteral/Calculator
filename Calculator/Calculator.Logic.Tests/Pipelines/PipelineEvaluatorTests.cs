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
            mConversionFacade = Substitute.For<IConversionFacade>();
            mSimplificationPipeline = Substitute.For<ISimplificationPipeline>();
            mApplicationArguments = Substitute.For<ApplicationArguments>();
        }

        Func<IConversionFacade> mConversionFactory;
        Func<ISimplificationPipeline> mSimplificationPipelineFactory;
        ApplicationArguments mApplicationArguments;
        PipelineEvaluator mPipelineEvaluator;
        IConversionFacade mConversionFacade;
        ISimplificationPipeline mSimplificationPipeline;

        [Test]
        public void If_Input_Is_Null_Pipeline_Returns_Null()
        {
            var input = "";
            var result = mPipelineEvaluator.Evaluate(input, mApplicationArguments);
            result.Should().Be(null);
        }
    }
}