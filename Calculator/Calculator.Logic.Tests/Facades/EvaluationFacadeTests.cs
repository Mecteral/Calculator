using System.Collections.Generic;
using Calculator.Logic.Facades;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Model;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
namespace Calculator.Logic.Tests.Facades
{
    [TestFixture]
    public class EvaluationFacadeTests
    {
        IModelBuilder mModelBuilder;
        IExpressionEvaluator mExpressionEvaluator;
        EvaluationFacade mUnderTest;

        [SetUp]
        public void Setup()
        {
            mModelBuilder = Substitute.For<IModelBuilder>();
            mExpressionEvaluator = Substitute.For<IExpressionEvaluator>();
            mUnderTest= new EvaluationFacade(mModelBuilder, mExpressionEvaluator);
        }
        [Test]
        public void Evaluate_Gets_Expression_From_ModelBuilder()
        {
            var tokenizer = Substitute.For<ITokenizer>();
            tokenizer.Tokens.Returns(new List<IToken>());

            mUnderTest.Evaluate(tokenizer, null);

            mModelBuilder.Received().BuildFrom(tokenizer.Tokens);
        }
        [Test]
        public void Evaluate_Passes_ModelBuilder_Result_To_ExpressionEvaluator()
        {
            var tokenizer = Substitute.For<ITokenizer>();
            var modelBuilderResult = new Constant();
            mModelBuilder.BuildFrom(tokenizer.Tokens).Returns(modelBuilderResult);

            mUnderTest.Evaluate(tokenizer, null);

            mExpressionEvaluator.Received().Evaluate(modelBuilderResult, null);
        }
        [Test]
        public void Evaluate_Returns_Result_Of_ExpressionEvaluator_Evaluate()
        {
            var tokenizer = Substitute.For<ITokenizer>();
            var modelBuilderResult = new Constant();
            mModelBuilder.BuildFrom(tokenizer.Tokens).Returns(modelBuilderResult);
            mExpressionEvaluator.Evaluate(modelBuilderResult, null).Returns(17M);

            mUnderTest.Evaluate(tokenizer, null).Should().Be(17M);
        }
    }
}