using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class MultiplicationByZeroRemovingSimplifierTests
    {
        [SetUp]
        public void SetUp()
        {
            mZeroRemover = new MultiplicationByZeroRemovingSimplifier();
        }

        readonly Multiplication mMultiplicationWithLeftZero = new Multiplication
        {
            Left = new Constant {Value = 0M},
            Right = new Sinus {Value = 13}
        };

        readonly Multiplication mMultiplicationWithRightZero = new Multiplication
        {
            Left = new Tangent {Value = 13M},
            Right = new Constant {Value = 0M}
        };

        MultiplicationByZeroRemovingSimplifier mZeroRemover;

        [Test]
        public void DoubleSidedZeroMultiplicationInSubtraction()
        {
            var expression = new Addition { Left = mMultiplicationWithLeftZero, Right = mMultiplicationWithRightZero };
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
        }

        [Test]
        public void LeftHandedZeroMultiplication()
        {
            var expression = new Addition { Left = mMultiplicationWithRightZero, Right = new Constant { Value = 13 } };
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(13M);
        }

        [Test]
        public void ParenthesedZeroBasedMultiplication()
        {
            var expression = new ParenthesedExpression { Wrapped = mMultiplicationWithLeftZero };
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(0M);
        }

        [Test]
        public void SimpleMultiplicationWithZeroLeft()
        {
            var expression = mMultiplicationWithLeftZero;
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
        }

        [Test]
        public void SimpleMultiplicationWithZeroRight()
        {
            var expression = mMultiplicationWithRightZero;
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
        }

        [Test]
        public void ZeroBasedMultiplicationRemover_Doesnt_Change_CosineAddition()
        {
            var expression = new Addition {Left = new Cosine {Value = 0}, Right = new Constant {Value = 0}};
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Cosine>()
                .Which.Value.Should()
                .Be(0);
            result.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(0);
        }

        [Test]
        public void ZeroBasedMultiplicationRemover_Doesnt_Change_SinusAddition()
        {
            var expression = new Addition {Left = new Sinus {Value = 0}, Right = new Constant {Value = 0}};
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Sinus>()
                .Which.Value.Should()
                .Be(0);
            result.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(0);
        }

        [Test]
        public void ZeroBasedMultiplicationRemover_Doesnt_Change_Square()
        {
            var expression = new Power {Left = new Constant {Value = 13}, Right = new Constant {Value = 0}};
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(0);
        }

        [Test]
        public void ZeroBasedMultiplicationRemover_Doesnt_Change_TangentSubtraction()
        {
            var expression = new Subtraction
            {
                Left = new Tangent {Value = 0},
                Right = new Constant {Value = 0}
            };
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Subtraction>()
                .Which.Left.Should()
                .BeOfType<Tangent>()
                .Which.Value.Should()
                .Be(0);
            result.Should()
                .BeOfType<Subtraction>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(0);
        }

        [Test]
        public void ZeroBasedMultiplicationRemover_Doesnt_Change_VariableAddition()
        {
            var expression = new Addition
            {
                Left = new Variable {Name = "a"},
                Right = new Constant(){Value = 0}
            };
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Variable>()
                .Which.Name.Should()
                .Be("a");
            result.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(0);
        }
    }
}
