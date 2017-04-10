using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class TreeDepthSetterTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new TreeDepthSetter();
        }

        TreeDepthSetter mUnderTest;

        [Test]
        public void Addition_Tree_Depth_Is_Set_Correctly()
        {
            var input = new Addition {Left = new Constant(), Right = new Sinus()};
            mUnderTest.SetTreeDepth(input);
            input.Should().BeOfType<Addition>().Which.TreeDepth.Should().Be(0);
            input.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Constant>().Which.TreeDepth.Should().Be(1);
            input.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Sinus>().Which.TreeDepth.Should().Be(1);
        }

        [Test]
        public void Complex_Case()
        {
            var input = new Addition
            {
                Left = new Subtraction {Left = new Sinus(), Right = new Cosine()},
                Right = new Division {Left = new Tangent(), Right = new Variable()}
            };
            mUnderTest.SetTreeDepth(input);
            input.Should().BeOfType<Addition>().Which.TreeDepth.Should().Be(0);
            input.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Subtraction>()
                .Which.TreeDepth.Should()
                .Be(1);
            input.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Division>().Which.TreeDepth.Should().Be(1);
            input.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Division>()
                .Which.Left.Should()
                .BeOfType<Tangent>()
                .Which.TreeDepth.Should()
                .Be(2);
            input.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Division>()
                .Which.Right.Should()
                .BeOfType<Variable>()
                .Which.TreeDepth.Should()
                .Be(2);
            input.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Subtraction>()
                .Which.Left.Should()
                .BeOfType<Sinus>()
                .Which.TreeDepth.Should()
                .Be(2);
            input.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Subtraction>()
                .Which.Right.Should()
                .BeOfType<Cosine>()
                .Which.TreeDepth.Should()
                .Be(2);
        }

        [Test]
        public void Complex_Case_With_Parenthesed()
        {
            var input = new ParenthesedExpression
            {
                Wrapped =
                    new Power
                    {
                        Left = new Multiplication {Left = new Constant(), Right = new Constant()},
                        Right = new Constant()
                    }
            };
            mUnderTest.SetTreeDepth(input);
            input.Should().BeOfType<ParenthesedExpression>().Which.TreeDepth.Should().Be(0);
            input.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Power>()
                .Which.TreeDepth.Should()
                .Be(1);
            input.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Power>()
                .Which.Left.Should().BeOfType<Multiplication>().Which.TreeDepth.Should().Be(2);
            input.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.TreeDepth.Should()
                .Be(3);
            input.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.TreeDepth.Should()
                .Be(3);
            input.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Power>()
                .Which.Right.Should().BeOfType<Constant>().Which.TreeDepth.Should().Be(2);
        }
    }
}