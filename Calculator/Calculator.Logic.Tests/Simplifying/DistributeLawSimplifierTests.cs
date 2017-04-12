using Calculator.Logic.Simplifying;
using Calculator.Logic.Utilities;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class DistributeLawSimplifierTests
    {
        [SetUp]
        public void SetUp()
        {
            mHelper = new DistributeLawHelper();
            mUnderTest = new DistributeLawSimplifier(mHelper);
        }

        DistributeLawHelper mHelper;
        DistributeLawSimplifier mUnderTest;

        [Test]
        public void Multplication_Bound_In_Addition_Doesnt_Change_Addition()
        {
            var input = new Addition
            {
                Left =
                    new Multiplication
                    {
                        Left =
                            new ParenthesedExpression
                            {
                                Wrapped =
                                    new Division {Left = new Constant {Value = 13}, Right = new Constant {Value = 17}}
                            },
                        Right = new Constant {Value = 2}
                    },
                Right = new Constant {Value = 31}
            };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(31);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Division>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Division>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(2);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Division>()
                .Which.Right.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(17);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Division>()
                .Which.Right.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(2);
        }

        [Test]
        public void Simple_Case()
        {
            var input = new Multiplication
            {
                Left =
                    new ParenthesedExpression
                    {
                        Wrapped = new Addition {Left = new Constant {Value = 13}, Right = new Constant {Value = 17}}
                    },
                Right = new Constant {Value = 2}
            };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>().Which.Value.Should().Be(13);
            result.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>().Which.Value.Should().Be(2);
            result.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>().Which.Value.Should().Be(17);
            result.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>().Which.Value.Should().Be(2);
        }

        [Test]
        public void Simple_Two_Sided_Parentheses_Multiplication()
        {
            var input = new Multiplication
            {
                Left =
                    new ParenthesedExpression
                    {
                        Wrapped = new Addition {Left = new Sinus {Value = 13}, Right = new Cosine {Value = 17}}
                    },
                Right =
                    new ParenthesedExpression
                    {
                        Wrapped = new Subtraction {Left = new Tangent {Value = 19}, Right = new Variable {Name = "x"}}
                    }
            };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Subtraction>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Tangent>()
                .Which.Value.Should()
                .Be(19);
            result.Should()
                .BeOfType<Subtraction>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Cosine>()
                .Which.Value.Should()
                .Be(17);
            result.Should()
                .BeOfType<Subtraction>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Sinus>()
                .Which.Value.Should().Be(13);
            result.Should()
                .BeOfType<Subtraction>()
                .Which.Right.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Variable>()
                .Which.Name.Should()
                .Be("x");
        }
    }
}