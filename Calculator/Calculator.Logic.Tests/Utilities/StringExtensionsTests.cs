using Calculator.Logic.Utilities;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Utilities
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("", "")]
        [TestCase("a", "a")]
        [TestCase("  a", "a")]
        [TestCase("a   ", "a")]
        [TestCase("a c", "ac")]
        [TestCase("a\tc", "ac")]
        [TestCase("a\rc", "ac")]
        [TestCase("a\nc", "ac")]
        [TestCase("a\n  \t\rc", "ac")]
        public void WithoutAnyWhitespace(string input, string expected)
        {
            input.WithoutAnyWhitespace().Should().Be(expected);
        }
    }
}