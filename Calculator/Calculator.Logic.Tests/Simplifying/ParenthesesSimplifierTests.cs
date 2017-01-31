using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplification
{
    [TestFixture]
    public class ParenthesesSimplifierTests
    {
        ParenthesesSimplifier mUnderTest;
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new ParenthesesSimplifier();
        }

        [Test]
        public void Parentheses_Deletion_Before_Multiplication()
        {
            var expression = new Multiplication()
            {
                Left = new ParenthesedExpression() {Wrapped = new Constant() {Value = 3M}},
                Right = new Constant() {Value = 13M}
            };
            mUnderTest.Simplify(expression).Should().BeOfType<Multiplication>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
        }
        [Test]
        public void Parentheses_Deletion_Before_Division()
        {
            var expression = new Division()
            {
                Left = new ParenthesedExpression() { Wrapped = new Constant() { Value = 3M } },
                Right = new Constant() { Value = 13M }
            };
            mUnderTest.Simplify(expression).Should().BeOfType<Division>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
        }
        [Test]
        public void Parentheses_Deletion_Before_Addition()
        {
            var expression = new Addition()
            {
                Left = new ParenthesedExpression() { Wrapped = new Constant() { Value = 3M } },
                Right = new SinusExpression() { Value = 13M }
            };
            mUnderTest.Simplify(expression).Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
        }
        [Test]
        public void Parentheses_Deletion_Before_Subtraction()
        {
            var expression = new Subtraction()
            {
                Left = new ParenthesedExpression() { Wrapped = new Constant() { Value = 3M } },
                Right = new Constant() { Value = 13M }
            };
            mUnderTest.Simplify(expression).Should().BeOfType<Subtraction>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
        }
        [Test]
        public void ParenthesesSimplifier_Doesnt_Remove_Paretheses_From_Nested_Expressions_With_Operations()
        {
            var expression = new ParenthesedExpression()
            {
                Wrapped =
                    new Addition()
                    {
                        Left = new CosineExpression() {Value = 12},
                        Right = new TangentExpression() {Value = 12}
                    }
            };
            mUnderTest.Simplify(expression).Should().BeOfType<ParenthesedExpression>().Which.Wrapped.Should().BeOfType<Addition>();
        }
    }
}