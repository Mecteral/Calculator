using Calculator.Logic.Simplifying;
using Calculator.Logic.Utilities;
using Calculator.Model;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class SimplifierTests
    {
        [SetUp]
        public void Setup()
        {
            mChecker = Substitute.For<IExpressionEqualityChecker>();
            mFirstSimplifier = Substitute.For<ISimplifier>();
            mSecondSimplifier = Substitute.For<ISimplifier>();
            mUnderTest = new Simplifier(new[] {mFirstSimplifier, mSecondSimplifier}, mChecker);
        }

        IExpressionEqualityChecker mChecker;
        ISimplifier mFirstSimplifier;
        ISimplifier mSecondSimplifier;
        Simplifier mUnderTest;

        [Test]
        public void Simplify_Calls_All_Passed_Simplifiers()
        {
            mChecker.IsEqual(Arg.Any<IExpression>(), Arg.Any<IExpression>()).Returns(true);
            var afterFirst = new Constant();
            var input = new Constant();
            mFirstSimplifier.Simplify(input).Returns(afterFirst);
            mUnderTest.Simplify(input);

            mSecondSimplifier.Received().Simplify(afterFirst);
        }

        [Test]
        public void Simplify_Returns_Input_If_No_Simplifier_Has_Changed_Anything()
        {
            mChecker.IsEqual(Arg.Any<IExpression>(), Arg.Any<IExpression>()).Returns(true);
            var afterFirst = new Constant {Value = 1};
            var afterSecond = new Constant {Value = 2};
            var input = new Constant {Value = 3};
            mFirstSimplifier.Simplify(input).Returns(afterFirst);
            mSecondSimplifier.Simplify(afterFirst).Returns(afterSecond);

            var result = mUnderTest.Simplify(input);
            result.Should().BeSameAs(input);
        }

        [Test]
        public void Simplify_Returns_Output_Of_Last_Simplifier_If_Has_Changed()
        {
            mChecker.IsEqual(Arg.Any<IExpression>(), Arg.Any<IExpression>()).Returns(false, true);
            var afterFirst = new Constant {Value = 1};
            var afterSecond = new Constant {Value = 2};
            var input = new Constant {Value = 3};
            mFirstSimplifier.Simplify(input).Returns(afterFirst);
            mSecondSimplifier.Simplify(afterFirst).Returns(afterSecond);

            var result = mUnderTest.Simplify(input);
            result.Should().BeSameAs(afterSecond);
        }
    }
}