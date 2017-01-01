using Calculator.Logic.Model;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Model
{
    [TestFixture]
    public class ExpressionExtensionsTests
    {
        [Test]
        public void Parenthesize()
        {
            var input = new Constant();
            var output = input.Parenthesize();
            output.Wrapped.Should().Be(input);
        }
    }
}