using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class SimplifierTests
    {
        static void Check(string underTest, string expected)
        {
            var tokenizer = new Tokenizer(underTest);
            tokenizer.Tokenize();
            var simplified = UseSimplifier(tokenizer.Tokens);
            simplified.Simplify().Should().Be(expected);
        }
        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);
        static Simplifier UseSimplifier(IEnumerable<IToken> tokens) => new Simplifier(CreateInMemoryModel(tokens));
        [Test]
        public void Simplification_Of_Nested_Additions()
        {
            Check("2+2+2+2a", "6 + 2*a");
        }
        [Test]
        public void Simplification_Of_ParenthesedExpression()
        {
            Check("2a+(3+2)", "2*a + (5)");
        }
        [Test]
        public void Simplification_Without_Variables()
        {
            Check("2-2", "0");
        }
    }
}