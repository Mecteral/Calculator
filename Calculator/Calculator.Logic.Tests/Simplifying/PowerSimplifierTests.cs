using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class PowerSimplifierTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new PowerSimplifier();
        }

        PowerSimplifier mUnderTest;

        [Test]
        public void PowerSimplifier_Changes_Multiplication_To_Power_If_Multiplication_Sides_Are_Of_Same_Value()
        {
            var input = new Multiplication {Left = new Constant {Value = 13}, Right = new Constant {Value = 13}};
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
        }

        [Test]
        public void
            PowerSimplifier_Does_Not_Change_Multiplication_To_Power_If_Multiplication_Sides_Are_Not_Of_Same_Value()
        {
            var input = new Multiplication {Left = new Constant {Value = 17}, Right = new Constant {Value = 13}};
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(17);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(13);
        }

        [Test]
        public void PowerSimplifier_Finds_Underlying_Multiplication_And_Changes_It_To_Power_Where_Possible()
        {
            var input = new Addition
            {
                Left = new Multiplication {Left = new Constant {Value = 17}, Right = new Constant {Value = 13}},
                Right = new Multiplication {Left = new Constant {Value = 13}, Right = new Constant {Value = 13}}
            };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Multiplication>();
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Power>();
        }
        [Test]
        public void PowerSimplifier_Finds_Underlying_Multiplication_And_Changes_It_To_Power_Where_Possible_Even_Multiple_Multiplications()
        {
            var input = new Addition
            {
                Left = new Multiplication { Left = new Constant { Value = 13 }, Right = new Constant { Value = 13 } },
                Right = new Multiplication { Left = new Constant { Value = 13 }, Right = new Constant { Value = 13 } }
            };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Power>();
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Power>();
        }
    }
}