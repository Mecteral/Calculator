using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Model.Tests
{
    [TestFixture]
    public class ParenthesedExpressionTests
    {
        [Test]
        public void Setting_Wrapped_Sets_Parent_Of_Value()
        {
            var underTest = new ParenthesedExpression {Wrapped = new Constant()};

            underTest.Wrapped.Parent.Should().BeSameAs(underTest);
        }
    }
}