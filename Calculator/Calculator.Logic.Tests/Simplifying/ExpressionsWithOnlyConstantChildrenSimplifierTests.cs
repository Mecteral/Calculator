using System.Collections.Generic;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class ExpressionsWithOnlyConstantChildrenSimplifierTests
    {
        static IEnumerable<IToken> Tokenize(string input)
        {
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(input, null);
            var tokens = tokenizer.Tokens;
            return tokens;
        }
        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);
        [Test]
        public void Cosine_Is_Replaced_By_Result()
        {
            var input = new Cosine {Value = 9};
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(input)
                .Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(9);
        }
        [Test]
        public void Division_Is_Replaced_By_Result()
        {
            var input = new Division {Left = new Constant {Value = 18}, Right = new Constant {Value = 9}};
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(input)
                .Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(2);
        }
        [Test]
        public void Negative_With_Single_Subtraction()
        {
            var expression = new Subtraction {Left = new Constant {Value = 1}, Right = new Constant {Value = -2}};
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(expression).Should().BeOfType<Constant>();
        }
        [Test]
        public void Power_Is_Replaced_By_Result()
        {
            var input = new Power {Left = new Constant {Value = 2}, Right = new Constant {Value = 7}};
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(input)
                .Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(128);
        }
        [Test]
        public void Simplification_Of_Nested_Additions()
        {
            var expression = new Addition {Left = new Constant {Value = 3}, Right = new Constant {Value = 3}};
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(expression).Should().BeOfType<Constant>();
        }
        [Test]
        public void Simplification_Of_ParenthesedExpression()
        {
            var expression = new Addition
            {
                Left = new Multiplication {Left = new Constant {Value = 3}, Right = new Variable {Variables = "a"}},
                Right =
                    new ParenthesedExpression
                    {
                        Wrapped = new Multiplication {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}}
                    }
            };
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(expression)
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
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(input);
            ((Addition) input).Left.Should().BeOfType<Addition>();
        }
        [Test]
        public void Sinus_Is_Replaced_By_Result()
        {
            var input = new Sinus {Value = 9};
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(input)
                .Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(9);
        }
        [Test]
        public void Subtraction_Changes_With_Negative_Righthandside()
        {
            var expression = new Division
            {
                Left = new Constant {Value = 1},
                Right = new Subtraction {Left = new Constant {Value = 2}, Right = new Constant {Value = -2}}
            };
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(expression)
                .Should()
                .BeOfType<Division>()
                .Which.Right.Should()
                .BeOfType<Constant>();
        }
        [Test]
        public void Tangent_Is_Replaced_By_Result()
        {
            var input = new Tangent {Value = 9};
            new ExpressionsWithOnlyConstantChildrenSimplifier().Simplify(input)
                .Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(9);
        }
    }
}