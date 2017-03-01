using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class DirectCalculationSimplifierTests
    {
        [SetUp]
        public void Setup()
        {
            mEvaluator = Substitute.For<IExpressionEvaluator>();
            mSimplifier = new DirectCalculationSimplifier(mEvaluator);
        }

        IExpressionEvaluator mEvaluator;
        DirectCalculationSimplifier mSimplifier;

        static IEnumerable<IToken> Tokenize(string input)
        {
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(input, null);
            var tokens = tokenizer.Tokens;
            return tokens;
        }

        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);


        [Test]
        public void Simplification_Of_Nested_Additions()
        {
            var expression = new Addition {Left = new Constant {Value = 3}, Right = new Constant {Value = 3}};
            mSimplifier.Simplify(expression).Should().BeOfType<Constant>();
        }

        [Test]
        public void Subtraction_Changes_With_Negative_Righthandside()
        {
            var expression = new Division() {Left = new Constant() {Value = 1}, Right = new Subtraction() {Left = new Constant() {Value = 2}, Right = new Constant() {Value = -2} } };
            mSimplifier.Simplify(expression).Should().BeOfType<Division>().Which.Right.Should().BeOfType<Constant>();
        }

        [Test]
        public void Negative_With_Single_Subtraction()
        {
            var expression =  new Subtraction() {Left = new Constant() {Value = 1}, Right = new Constant() {Value = -2} };
            mSimplifier.Simplify(expression).Should().BeOfType<Constant>();
        }
        [Test]
        public void Simplification_Of_ParenthesedExpression()
        {
            var expression = new Addition
            {
                Left =
                    new Multiplication
                    {
                        Left = new Constant {Value = 3},
                        Right = new Variable {Variables = "a"}
                    },
                Right =
                    new ParenthesedExpression
                    {
                        Wrapped =
                            new Multiplication
                            {
                                Left = new Constant
                                {
                                    Value = 1
                                },
                                Right = new Constant {Value = 2}
                            }
                    }
            };
            mSimplifier.Simplify(expression)
                .Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Constant>();
        }

        [Test]
        public void Simplify_Does_Not_Change_Input_Expression_Tree()
        {
            var input = CreateInMemoryModel(Tokenize("2+2+2+2a"));
            var underTest = new DirectCalculationSimplifier(mEvaluator);
            underTest.Simplify
            (
                input
            );
            ((
                    Addition
                    )
                    input
                ).
                Left.Should
                ().
                BeOfType<Addition>
                ();
        }
    }
}