using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class NeutralElementEliminatingSimplifierTests
    {
        [Test]
        public void Multiplication_By_One_With_One_As_Left_Side_Returns_Copy_Of_Right_Side()
        {
            var input = new Multiplication {Left = new Constant {Value = 1}, Right = new Variable {Name = "x"}};
            var underTest = new NeutralElementEliminatingSimplifier();

            var output = underTest.Simplify(input);
            output.Should().BeOfType<Variable>().Which.Name.Should().Be("x");
            output.Should().NotBeSameAs(input.Right);
        }

        [Test]
        public void Multiplication_By_One_With_One_As_Right_Side_Returns_Copy_Of_Left_Side()
        {
            var input = new Multiplication {Right = new Constant {Value = 1}, Left = new Variable {Name = "x"}};
            var underTest = new NeutralElementEliminatingSimplifier();

            var output = underTest.Simplify(input);
            output.Should().BeOfType<Variable>().Which.Name.Should().Be("x");
            output.Should().NotBeSameAs(input.Left);
        }
    }
}