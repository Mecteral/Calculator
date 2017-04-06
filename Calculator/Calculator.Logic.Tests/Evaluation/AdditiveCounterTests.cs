using Calculator.Logic.Evaluation;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Evaluation
{
    [TestFixture]
    public class AdditiveCounterTests
    {
        AdditiveCounter mUnderTest;


        Power Power(int left, int right)
            => new Power {Left = new Constant {Value = left}, Right = new Constant {Value = right}};

        Addition Addition(int left, int right)
            => new Addition {Left = new Constant {Value = left}, Right = new Constant {Value = right}};

        Subtraction Subtraction(int left, int right)
            => new Subtraction {Left = new Constant {Value = left}, Right = new Constant {Value = right}};

        ParenthesedExpression ParenthesedExpression(IExpression expression)
            => new ParenthesedExpression {Wrapped = expression};

        [SetUp]
        public void SetUp()
        {
            mUnderTest = new AdditiveCounter();
        }

        void Check(IExpression input, int expected)
        {
            mUnderTest.Evaluate(input).Should().Be(expected);
        }
        [Test]
        public void Solo_Addition_Count_Is_Correct()
        {
            Check(Addition(13, 17), 1);
        }

        [Test]
        public void Solo_Subtraction_Count_Is_Correct()
        {
            Check(Subtraction(13, 17), 1);
        }

        [Test]
        public void Complex_Case()
        {
            var input = new Multiplication() {Left = Addition(13, 17), Right = new Division() {Left = Subtraction(13, 17), Right = ParenthesedExpression(Power(13, 17)) } };
            Check(input, 2);
        }
    }
}