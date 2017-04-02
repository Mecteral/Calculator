using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Model.Tests
{
    public class ExpressionExtensionsTests
    {
        [Test]
        public void Parenthesize_ReturnsParenthesedExpression_With_Wrapped_Set_To_LeftHandSide()
        {
            var input = new Constant();
            var output = input.Parenthesize();
            output.Wrapped.Should().Be(input);
        }
        [Test]
        public void Parent_Sets_Parent_To_Argument()
        {
            var input = new Constant();
            var father = new Constant();

            input.Parent(father);

            input.Parent.Should().BeSameAs(father);
        }
        [Test]
        public void IsZero_Returns_False_If_Not_A_Constant()
        {
            new Variable().IsZero().Should().BeFalse();
        }
        [Test]
        public void IsZero_Returns_False_If_Constant_Value_Not_Zero()
        {
            new Constant {Value = 2}.IsZero().Should().BeFalse();
        }
        [Test]
        public void IsZero_Returns_True_If_Constant_With_Value_Of_Zero()
        {
            new Constant {Value = 0}.IsZero().Should().BeTrue();
        }
    }
}