using System;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Model.Tests
{
    [TestFixture]
    public class ParenthesedExpressionTests
    {
        [Test]
        public void ReplaceChild_Sets_Parent_Of_OldChild_To_Null()
        {
            var oldChild = new Constant();
            var underTest = new ParenthesedExpression {Wrapped = oldChild};

            underTest.ReplaceChild(underTest.Wrapped, new Constant {Value = 13});

            oldChild.HasParent.Should().BeFalse();
        }
        [Test]
        public void ReplaceChild_Sets_Wrapped_To_NewChild()
        {
            var underTest = new ParenthesedExpression {Wrapped = new Constant()};

            underTest.ReplaceChild(underTest.Wrapped, new Constant {Value = 13});

            underTest.Wrapped.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
        }
        [Test]
        public void ReplaceChild_With_Argument_Which_Is_Not_Wrapped_Throws()
        {
            var underTest = new ParenthesedExpression {Wrapped = new Constant()};

            Action a = () => underTest.ReplaceChild(new Constant(), new Constant());
            a.ShouldThrow<ArgumentException>();
        }
        [Test]
        public void Setting_Wrapped_Sets_Parent_Of_Value()
        {
            var underTest = new ParenthesedExpression {Wrapped = new Constant()};

            underTest.Wrapped.Parent.Should().BeSameAs(underTest);
        }
    }
}