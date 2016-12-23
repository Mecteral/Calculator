using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Model.Tests
{
    [TestFixture(typeof(Addition))]
    [TestFixture(typeof(Subtraction))]
    [TestFixture(typeof(Multiplication))]
    [TestFixture(typeof(Division))]
    public class AnArithmeticOperationTests<T> where T : AnArithmeticOperation, new()
    {
        [Test]
        public void Setting_Left_Sets_Parent_Of_Value()
        {
            var underTest = new T {Left = new Constant()};

            underTest.Left.Parent.Should().BeSameAs(underTest);
        }
        [Test]
        public void Setting_Right_Sets_Parent_Of_Value()
        {
            var underTest = new T {Right = new Constant()};

            underTest.Right.Parent.Should().BeSameAs(underTest);
        }
    }
}