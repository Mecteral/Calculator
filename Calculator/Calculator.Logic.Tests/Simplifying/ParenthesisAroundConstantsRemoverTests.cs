using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class ParenthesisAroundConstantsRemoverTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest= new ParenthesisAroundConstantsRemover();
        }
        ParenthesisAroundConstantsRemover mUnderTest;

        [Test]
        public void Parentheses_Deletion_Before_Addition()
        {
            var expression = new Addition
            {
                Left = new ParenthesedExpression {Wrapped = new Constant {Value = 3M}},
                Right = new Sinus {Value = 13M}
            };
            mUnderTest.Simplify(expression)
                .Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(3);
        }

        [Test]
        public void Parentheses_Deletion_Before_Division()
        {
            var expression = new Division
            {
                Left = new ParenthesedExpression {Wrapped = new Constant {Value = 3M}},
                Right = new Constant {Value = 13M}
            };
            mUnderTest.Simplify(expression)
                .Should()
                .BeOfType<Division>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(3);
        }

        [Test]
        public void Parentheses_Deletion_Before_Multiplication()
        {
            var expression = new Multiplication
            {
                Left = new ParenthesedExpression {Wrapped = new Constant {Value = 3M}},
                Right = new Constant {Value = 13M}
            };
            mUnderTest.Simplify(expression)
                .Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(3);
        }

        [Test]
        public void Parentheses_Deletion_Before_Subtraction()
        {
            var expression = new Subtraction
            {
                Left = new ParenthesedExpression {Wrapped = new Constant {Value = 3M}},
                Right = new Constant {Value = 13M}
            };
            mUnderTest.Simplify(expression)
                .Should()
                .BeOfType<Subtraction>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(3);
        }

        [Test]
        public void SimpleSquareTest()
        {
            var expression = new ParenthesedExpression() {Wrapped = new Addition() {Left = new Power() { Left = new Constant() { Value = 2 }, Right = new Constant() { Value = 2 } } , Right = new Constant() {Value = 16} }  };
            mUnderTest.Simplify(expression).Should().BeOfType<ParenthesedExpression>().Which.Wrapped.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
            mUnderTest.Simplify(expression).Should().BeOfType<ParenthesedExpression>().Which.Wrapped.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
            mUnderTest.Simplify(expression).Should().BeOfType<ParenthesedExpression>().Which.Wrapped.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(16);
        }

        [Test]
        public void RightHandedParentheses()
        {
            var expression = new Addition() {Left = new Constant() {Value = 1}, Right = new ParenthesedExpression() {Wrapped = new Constant() {Value = 2} } };
            mUnderTest.Simplify(expression)
                .Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(1);
            mUnderTest.Simplify(expression)
                .Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(2);
        }

        [Test]
        public void ParenthesesSimplifier_Doesnt_Remove_Paretheses_From_Nested_Expressions_With_Operations()
        {
            var expression = new ParenthesedExpression
            {
                Wrapped =
                    new Addition
                    {
                        Left = new Cosine {Value = 12},
                        Right = new Tangent {Value = 12}
                    }
            };
            mUnderTest.Simplify(expression)
                .Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Addition>();
        }
    }
}