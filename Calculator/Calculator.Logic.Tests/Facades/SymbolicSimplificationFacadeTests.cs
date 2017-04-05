using Calculator.Logic.Facades;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Facades
{
    [TestFixture]
    public class SymbolicSimplificationFacadeTests
    {
        IExpressionFormatter mExpressionFormatter;
        ISimplifier mSimplifier;
        IModelBuilder mModelBuilder;
        SymbolicSimplificationFacade mUnderTest;

        [SetUp]
        public void Setup()
        {
            mModelBuilder = Substitute.For<IModelBuilder>();
            mSimplifier = Substitute.For<ISimplifier>();
            mExpressionFormatter = Substitute.For<IExpressionFormatter>();
            mUnderTest = new SymbolicSimplificationFacade(mExpressionFormatter, mSimplifier, mModelBuilder);
        }

        [Test]
        public void ModelBuilder_Receives_From_Input()
        {
            var tokenizer = Substitute.For<ITokenizer>();
            mUnderTest.Simplify(tokenizer);

            mModelBuilder.Received().BuildFrom(tokenizer.Tokens);
        }

        [Test]
        public void Simplifier_Receives_From_ModelBuilder()
        {
            var tokenizer = Substitute.For<ITokenizer>();
            var modelBuilderResult = new Constant();
            mModelBuilder.BuildFrom(tokenizer.Tokens).Returns(modelBuilderResult);
            mUnderTest.Simplify(tokenizer);

            mSimplifier.Received().Simplify(modelBuilderResult);
        }

        [Test]
        public void FormattingExpressionVisitor_Receives_From_Simplify()
        {
            var tokenizer = Substitute.For<ITokenizer>();
            var simplifierResult = new Constant();
            mSimplifier.Simplify(Arg.Any<IExpression>()).Returns(simplifierResult);

            mUnderTest.Simplify(tokenizer);

            mExpressionFormatter.Received().Format(simplifierResult);
        }

        [Test]
        public void FormattingExpressionVisitor_Return_Proper_String()
        {
            var tokenizer = Substitute.For<ITokenizer>();
            var expression = Substitute.For<IExpression>();
            mModelBuilder.BuildFrom(tokenizer.Tokens).Returns(expression);
            mSimplifier.Simplify(expression).Returns(expression);
            const string simplifyResult = "2a";
            mExpressionFormatter.Format(expression).Returns(simplifyResult);

            mUnderTest.Simplify(tokenizer).Should().Be(simplifyResult);
        }
    }
}