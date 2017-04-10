using Calculator.Logic.Evaluation;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Evaluation
{
    [TestFixture]
    public class TreeDepthCounterTests
    {
        [SetUp]
        public void SetUp()
        {
            mSetter = new TreeDepthSetter();
            mUnderTest = new TreeDepthCounter();
        }

        TreeDepthSetter mSetter;
        TreeDepthCounter mUnderTest;

        [Test]
        public void Simple_One_Sided_Tree_Count_Is_Correct()
        {
            var input = new Addition() {Left = new Constant(), Right = new Constant()};
            mSetter.SetTreeDepth(input);
            var result = mUnderTest.Evaluate(input);

            result.Should().Be(1);
        }

        [Test]
        public void Left_Sided_Tree_Count_Is_Correct()
        {
            var input = new Addition() {Left = new Subtraction() {Left = new Cosine(), Right = new Division() {Left = new Sinus(), Right = new Tangent()} }, Right = new Variable()};
            mSetter.SetTreeDepth(input);
            var result = mUnderTest.Evaluate(input);
            result.Should().Be(3);
        }
    }
}