using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class ZeroBasedMultiplicationRemoverTests
    {
        [SetUp]
        public void SetUp()
        {
            mZeroRemover = new ZeroBasedMultiplicationRemover();
        }

        readonly Multiplication mMultiplicationWithLeftZero = new Multiplication
        {
            Left = new Constant {Value = 0M},
            Right = new SinusExpression {Value = 13}
        };

        readonly Multiplication mMultiplicationWithRightZero = new Multiplication
        {
            Left = new TangentExpression {Value = 13M},
            Right = new Constant {Value = 0M}
        };

        ZeroBasedMultiplicationRemover mZeroRemover;

        [Test]
        public void DoubleSidedZeroMultiplicationInSubtraction()
        {
            var expression = new Addition {Left = mMultiplicationWithLeftZero, Right = mMultiplicationWithRightZero};
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
        }

        [Test]
        public void LeftHandedZeroMultiplication()
        {
            var expression = new Addition {Left = mMultiplicationWithRightZero, Right = new Constant {Value = 13}};
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(0M);
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(13M);
        }

        [Test]
        public void ParenthesedZeroBasedMultiplication()
        {
            var expression = new ParenthesedExpression {Wrapped = mMultiplicationWithLeftZero};
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
            var expression = new Addition {Left = new CosineExpression {Value = 0}, Right = new Constant {Value = 0}};
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<CosineExpression>()
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
            var expression = new Addition {Left = new SinusExpression {Value = 0}, Right = new Constant {Value = 0}};
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<SinusExpression>()
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
            var expression = new Square {Left = new Constant {Value = 13}, Right = new Constant {Value = 0}};
            var result = mZeroRemover.Simplify(expression);
            result.Should().BeOfType<Square>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
            result.Should().BeOfType<Square>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(0);
        }

        [Test]
        public void ZeroBasedMultiplicationRemover_Doesnt_Change_TangentSubtraction()
        {
            var expression = new Subtraction
            {
                Left = new TangentExpression {Value = 0},
                Right = new Constant {Value = 0}
            };
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Subtraction>()
                .Which.Left.Should()
                .BeOfType<TangentExpression>()
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
                Left = new Variable {Variables = "a"},
                Right = new SquareRootExpression {Value = 0}
            };
            var result = mZeroRemover.Simplify(expression);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Variable>()
                .Which.Variables.Should()
                .Be("a");
            result.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<SquareRootExpression>()
                .Which.Value.Should()
                .Be(0);
        }
    }
}